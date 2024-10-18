// Controllers/EmployeesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRSystemBackend.Data;
using HRSystemBackend.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace HRSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly HRSystemContext _context;

        public EmployeesController(HRSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Shift)
                .ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            var company = await _context.Companies.FindAsync(employee.ComID);
            if (company == null) return BadRequest("Invalid Company ID");

            employee.Basic = employee.Gross * (company.Basic / 100);
            employee.HRent = employee.Gross * (company.Hrent / 100);
            employee.Medical = employee.Gross * (company.Medical / 100);
            employee.Others = employee.Gross - (employee.Basic + employee.HRent + employee.Medical);

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmpID }, employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Shift)
                .FirstOrDefaultAsync(e => e.EmpID == id);

            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpGet("service-length")]
        public async Task<IActionResult> GetEmployeeServiceLength()
        {
            var today = DateTime.Now;
            var employees = await _context.Employees
                .Select(e => new
                {
                    e.EmpID,
                    e.EmpName,
                    e.DtJoin,
                    // Calculate months between join date and today
                    ServiceLength = ((today.Year - e.DtJoin.Year) * 12) + today.Month - e.DtJoin.Month
                })
                .ToListAsync();

            return Ok(employees);
        }
    }
}
