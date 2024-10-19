// Controllers/SalaryController.cs
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
    public class SalariesController : ControllerBase
    {
        private readonly HRSystemContext _context;

        public SalariesController(HRSystemContext context)
        {
            _context = context;
        }

        [HttpPost("calculate-monthly")]
        public async Task<IActionResult> CalculateMonthlySalary(int comId, int year, int month)
        {
            var employees = await _context.Employees
                .Where(e => e.ComID == comId)
                .ToListAsync();

            foreach (var employee in employees)
            {
                var attendance = await _context.AttendanceSummaries
                    .FirstOrDefaultAsync(a =>
                        a.ComID == comId &&
                        a.EmpID == employee.EmpID &&
                        a.DtYear == year &&
                        a.DtMonth == month);

                if (attendance != null)
                {
                    var salary = new Salary
                    {
                        ComID = comId,
                        EmpID = employee.EmpID,
                        DtYear = year,
                        DtMonth = month,
                        Gross = employee.Gross,
                        Basic = employee.Basic,
                        Hrent = employee.HRent,
                        Medical = employee.Medical,
                        AbsentAmount = (employee.Basic / 30) * attendance.Absent,
                        PayableAmount = employee.Gross - ((employee.Basic / 30) * attendance.Absent),
                        IsPaid = false,
                        PaidAmount = 0
                    };

                    _context.Salaries.Add(salary);
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Monthly salary calculated successfully");
        }

        [HttpPost("pay-salary")]
        public async Task<IActionResult> PaySalary(int comId, int empId, int year, int month)
        {
            var salary = await _context.Salaries
                .FirstOrDefaultAsync(s =>
                    s.ComID == comId &&
                    s.EmpID == empId &&
                    s.DtYear == year &&
                    s.DtMonth == month);

            if (salary == null) return NotFound("Salary record not found.");

            salary.IsPaid = true;
            salary.PaidAmount = salary.PayableAmount;

            await _context.SaveChangesAsync();
            return Ok("Salary paid successfully.");
        }


        [HttpGet("department-summary")]
        public async Task<IActionResult> GetDepartmentSalarySummary(int comId, int year, int month)
        {
            var summary = await _context.Salaries
                .Where(s => s.ComID == comId && s.DtYear == year && s.DtMonth == month)
                .GroupBy(s => s.Employee.Department.DeptName)
                .Select(g => new
                {
                    Department = g.Key,
                    TotalPayable = g.Sum(s => s.PayableAmount),
                    TotalPaid = g.Sum(s => s.PaidAmount),
                    EmployeeCount = g.Count()
                })
                .ToListAsync();

            return Ok(summary);
        }

        [HttpGet("unpaid-list")]
        public async Task<IActionResult> GetUnpaidSalaries(int comId, int year, int month)
        {
            var unpaidSalaries = await _context.Salaries
                .Where(s =>
                    s.ComID == comId &&
                    s.DtYear == year &&
                    s.DtMonth == month &&
                    !s.IsPaid)
                .Include(s => s.Employee)
                .Select(s => new
                {
                    s.Employee.EmpName,
                    s.PayableAmount,
                    s.DtYear,
                    s.DtMonth
                })
                .ToListAsync();

            return Ok(unpaidSalaries);
        }
    }
}