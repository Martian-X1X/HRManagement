// Controllers/ShiftsController.cs
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
    public class ShiftsController : ControllerBase
    {
        private readonly HRSystemContext _context;

        public ShiftsController(HRSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetShifts(int comId)
        {
            var shifts = await _context.Shifts
                .Where(s => s.ComID == comId)
                .ToListAsync();
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return NotFound();
            return Ok(shift);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift(Shift shift)
        {
            // Check if the Company ID exists
            if (!_context.Companies.Any(c => c.ComID == shift.ComID))
            {
                return BadRequest("Invalid Company ID");
            }

            shift.Company = await _context.Companies.FindAsync(shift.ComID);

            try
            {
                _context.Shifts.Add(shift);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetShift), new { id = shift.ShiftID }, shift);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details here
                return StatusCode(500, "An error occurred while creating the Shift.");
            }
        }




        [HttpGet("{id}/employees")]
        public async Task<IActionResult> GetShiftEmployees(int id)
        {
            var employees = await _context.Employees
                .Where(e => e.ShiftID == id)
                .Select(e => new
                {
                    e.EmpID,
                    e.EmpName,
                    e.Department.DeptName,
                    e.Designation.DesigName
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("late-summary")]
        public async Task<IActionResult> GetLateSummary(int comId, DateTime date)
        {
            var lateSummary = await _context.Attendances
                .Where(a =>
                    a.ComID == comId &&
                    a.DtDate.Date == date.Date &&
                    a.AttStatus == "Late")
                .GroupBy(a => a.Employee.Shift.ShiftName)
                .Select(g => new
                {
                    ShiftName = g.Key,
                    LateCount = g.Count(),
                    Employees = g.Select(a => new
                    {
                        a.Employee.EmpName,
                        a.InTime
                    }).ToList()
                })
                .ToListAsync();

            return Ok(lateSummary);
        }
    }
}