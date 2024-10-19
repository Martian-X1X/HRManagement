// Controllers/DepartmentController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRSystemBackend.Data;
using HRSystemBackend.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace HRSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly HRSystemContext _context;

        public DepartmentsController(HRSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments(int comId)
        {
            var departments = await _context.Departments
                .Where(d => d.ComID == comId)
                .ToListAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            // Validate if the Company ID exists before creating the department
            if (!_context.Companies.Any(c => c.ComID == department.ComID))
            {
                return BadRequest("Invalid Company ID");
            }

            // Optionally fetch the company details if needed
            department.Company = await _context.Companies.FindAsync(department.ComID);

            try
            {
                // Add the department without any employees
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDepartment), new { id = department.DeptID }, department);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details here if needed
                return StatusCode(500, "An error occurred while creating the department.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.DeptID) return BadRequest();
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/employees")]
        public async Task<IActionResult> GetDepartmentEmployees(int id)
        {
            var employees = await _context.Employees
                .Where(e => e.DeptID == id)
                .Select(e => new
                {
                    e.EmpID,
                    e.EmpName,
                    e.Designation.DesigName,
                    e.Gross
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetDepartmentSummary(int comId)
        {
            var summary = await _context.Departments
                .Where(d => d.ComID == comId)
                .Include(d => d.Employees)  // Include the Employees navigation property
                .Select(d => new
                {
                    DepartmentName = d.DeptName,
                    EmployeeCount = _context.Employees.Count(e => e.DeptID == d.DeptID),
                    TotalSalary = _context.Employees.Where(e => e.DeptID == d.DeptID).Sum(e => e.Gross)
                })
                .ToListAsync();

            return Ok(summary);
        }

        // Alternative approach using GroupBy if you prefer
        [HttpGet("summary-alt")]
        public async Task<IActionResult> GetDepartmentSummaryAlt(int comId)
        {
            var summary = await _context.Employees
                .Where(e => e.ComID == comId)
                .GroupBy(e => new { e.DeptID, e.Department.DeptName })
                .Select(g => new
                {
                    DepartmentName = g.Key.DeptName,
                    EmployeeCount = g.Count(),
                    TotalSalary = g.Sum(e => e.Gross)
                })
                .ToListAsync();

            return Ok(summary);
        }
    }
}
