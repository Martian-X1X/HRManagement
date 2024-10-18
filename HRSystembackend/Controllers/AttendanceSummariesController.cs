// Controllers/AttendanceSummariesController.cs
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
    public class AttendanceSummariesController : ControllerBase
    {
        private readonly HRSystemContext _context;

        public AttendanceSummariesController(HRSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendanceSummaries(int comId, int year, int month)
        {
            var summaries = await _context.AttendanceSummaries
                .Where(a => a.ComID == comId && a.DtYear == year && a.DtMonth == month)
                .Include(a => a.Employee)
                .Select(a => new
                {
                    a.Employee.EmpName,
                    a.DtYear,
                    a.DtMonth,
                    a.Absent
                })
                .ToListAsync();

            return Ok(summaries);
        }

        [HttpGet("department-wise")]
        public async Task<IActionResult> GetDepartmentWiseSummary(int comId, int year, int month)
        {
            var summaries = await _context.AttendanceSummaries
                .Where(a => a.ComID == comId && a.DtYear == year && a.DtMonth == month)
                .Include(a => a.Employee)
                .Include(a => a.Employee.Department)
                .GroupBy(a => a.Employee.Department.DeptName)
                .Select(g => new
                {
                    Department = g.Key,
                    TotalAbsent = g.Sum(a => a.Absent),
                    EmployeeCount = g.Count(),
                    AverageAbsence = (double)g.Sum(a => a.Absent) / g.Count()
                })
                .ToListAsync();

            return Ok(summaries);
        }

        [HttpGet("{empId}")]
        public async Task<IActionResult> GetEmployeeSummary(int empId, int year)
        {
            var summaries = await _context.AttendanceSummaries
                .Where(a => a.EmpID == empId && a.DtYear == year)
                .OrderBy(a => a.DtMonth)
                .Select(a => new
                {
                    a.DtMonth,
                    a.Absent
                })
                .ToListAsync();

            return Ok(summaries);
        }
    }
}