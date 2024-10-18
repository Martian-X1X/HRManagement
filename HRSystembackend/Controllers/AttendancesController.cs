// Controllers/AttendanceController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRSystemBackend.Data;
using HRSystemBackend.Models;
using System.Threading.Tasks;
using System.Linq;

namespace HRSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendancesController : ControllerBase
    {
        private readonly HRSystemContext _context;

        public AttendancesController(HRSystemContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RecordAttendance(Attendance attendance)
        {
            var shift = await _context.Shifts.FindAsync(attendance.Employee.ShiftID);
            if (shift == null) return BadRequest("Invalid Shift");

            if (attendance.InTime > shift.ShiftIn.Add(shift.ShiftLate))
            {
                attendance.AttStatus = "Late";
            }
            else
            {
                attendance.AttStatus = "Present";
            }

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            await UpdateAttendanceSummary(attendance);

            return Ok(attendance);
        }

        private async Task UpdateAttendanceSummary(Attendance attendance)
        {
            var summary = await _context.AttendanceSummaries
                .FirstOrDefaultAsync(a =>
                    a.ComID == attendance.ComID &&
                    a.EmpID == attendance.EmpID &&
                    a.DtYear == attendance.DtDate.Year &&
                    a.DtMonth == attendance.DtDate.Month);

            if (summary == null)
            {
                summary = new AttendanceSummary
                {
                    ComID = attendance.ComID,
                    EmpID = attendance.EmpID,
                    DtYear = attendance.DtDate.Year,
                    DtMonth = attendance.DtDate.Month,
                    Absent = attendance.AttStatus == "Absent" ? 1 : 0
                };
                _context.AttendanceSummaries.Add(summary);
            }
            else if (attendance.AttStatus == "Absent")
            {
                summary.Absent++;
            }

            await _context.SaveChangesAsync();
        }

        [HttpGet("daily-report")]
        public async Task<IActionResult> GetDailyAttendance(int comId, DateTime date)
        {
            var attendance = await _context.Attendances
                .Where(a => a.ComID == comId && a.DtDate.Date == date.Date)
                .Include(a => a.Employee)
                .Select(a => new
                {
                    a.Employee.EmpName,
                    a.AttStatus,
                    a.InTime,
                    a.OutTime
                })
                .ToListAsync();

            return Ok(attendance);
        }

        [HttpGet("monthly-report")]
        public async Task<IActionResult> GetMonthlyAttendance(int comId, int year, int month)
        {
            var summary = await _context.AttendanceSummaries
                .Where(a => a.ComID == comId && a.DtYear == year && a.DtMonth == month)
                .Include(a => a.Employee)
                .Include(a => a.Employee.Department)
                .GroupBy(a => a.Employee.Department.DeptName)
                .Select(g => new
                {
                    Department = g.Key,
                    TotalAbsent = g.Sum(a => a.Absent),
                    EmployeeCount = g.Count()
                })
                .ToListAsync();

            return Ok(summary);
        }
    }
}
