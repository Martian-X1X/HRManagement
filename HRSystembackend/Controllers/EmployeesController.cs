// Controllers/EmployeesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRSystemBackend.Data;
using HRSystemBackend.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> CreateEmployee(
            [Required] int ComID,
            [Required] int ShiftID,
            [Required] int DeptID,
            [Required] int DesigID,
            [Required, StringLength(50)] string EmpCode,
            [Required, StringLength(100)] string EmpName,
            string Gender,
            [Required] decimal Gross,
            [Required] DateTime DtJoin) // Ensure this is in UTC
        {
            // Validate if the Company, Designation, Shift, and Department IDs are valid
            if (!_context.Companies.Any(c => c.ComID == ComID))
            {
                return BadRequest("Invalid Company ID");
            }
            if (!_context.Designations.Any(d => d.DesigID == DesigID))
            {
                return BadRequest("Invalid Designation ID");
            }
            if (!_context.Shifts.Any(s => s.ShiftID == ShiftID))
            {
                return BadRequest("Invalid Shift ID");
            }
            if (!_context.Departments.Any(d => d.DeptID == DeptID))
            {
                return BadRequest("Invalid Department ID");
            }

            // Fetch the company to calculate salary components
            var company = await _context.Companies.FindAsync(ComID);
            if (company == null) return BadRequest("Company not found");

            // Create the Employee object
            var employee = new Employee
            {
                ComID = ComID,
                ShiftID = ShiftID,
                DeptID = DeptID,
                DesigID = DesigID,
                EmpCode = EmpCode,
                EmpName = EmpName,
                Gender = Gender,
                Gross = Gross,
                DtJoin = DtJoin.ToUniversalTime() // Convert to UTC before saving
            };

            // Calculate salary components based on the company-defined percentages
            employee.Basic = employee.Gross * ((decimal)50 / 100);   // Basic salary as a percentage of gross
            employee.HRent = employee.Gross * ((decimal)30 / 100);   // House rent as a percentage of gross
            employee.Medical = employee.Gross * ((decimal)15 / 100); // Medical allowance as a percentage of gross


            // Calculate the "Others" component by subtracting the sum of other components from the gross salary
            employee.Others = employee.Gross - (employee.Basic + employee.HRent + employee.Medical);

            // Add the employee to the database
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Return a success response with the created employee details
            var response = new
            {
                employee.EmpID,
                employee.EmpCode,
                employee.EmpName,
                employee.Gross,
                employee.Basic,
                employee.HRent,
                employee.Medical,
                employee.Others,
                employee.DtJoin
            };

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmpID }, response);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id,
    [Required] int ComID,
    [Required] int ShiftID,
    [Required] int DeptID,
    [Required] int DesigID,
    [Required, StringLength(50)] string EmpCode,
    [Required, StringLength(100)] string EmpName,
    string Gender,
    [Required] decimal Gross,
    [Required] DateTime DtJoin)
        {
            // Find the existing employee
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Validate if the Company, Designation, Shift, and Department IDs are valid
            if (!_context.Companies.Any(c => c.ComID == ComID))
            {
                return BadRequest("Invalid Company ID");
            }
            if (!_context.Designations.Any(d => d.DesigID == DesigID))
            {
                return BadRequest("Invalid Designation ID");
            }
            if (!_context.Shifts.Any(s => s.ShiftID == ShiftID))
            {
                return BadRequest("Invalid Shift ID");
            }
            if (!_context.Departments.Any(d => d.DeptID == DeptID))
            {
                return BadRequest("Invalid Department ID");
            }

            // Update the employee's properties
            employee.ComID = ComID;
            employee.ShiftID = ShiftID;
            employee.DeptID = DeptID;
            employee.DesigID = DesigID;
            employee.EmpCode = EmpCode;
            employee.EmpName = EmpName;
            employee.Gender = Gender;
            employee.Gross = Gross;
            employee.DtJoin = DtJoin.ToUniversalTime(); // Convert to UTC

            // Recalculate salary components based on the company-defined percentages
            var company = await _context.Companies.FindAsync(ComID);
            employee.Basic = employee.Gross * ((decimal)50 / 100);   // Basic salary as a percentage of gross
            employee.HRent = employee.Gross * ((decimal)30 / 100);   // House rent as a percentage of gross
            employee.Medical = employee.Gross * ((decimal)15 / 100); // Medical allowance as a percentage of gross
            employee.Others = employee.Gross - (employee.Basic + employee.HRent + employee.Medical);

            // Update the employee in the database
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            // Return a success response with the updated employee details
            var response = new
            {
                employee.EmpID,
                employee.EmpCode,
                employee.EmpName,
                employee.Gross,
                employee.Basic,
                employee.HRent,
                employee.Medical,
                employee.Others,
                employee.DtJoin
            };

            return Ok(response);
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
