// Controllers/DesignationController.cs
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
    public class DesignationsController : ControllerBase
    {
        private readonly HRSystemContext _context;

        public DesignationsController(HRSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDesignations(int comId)
        {
            var designations = await _context.Designations
                .Where(d => d.ComID == comId)
                .ToListAsync();
            return Ok(designations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesignation(int id)
        {
            var designation = await _context.Designations.FindAsync(id);
            if (designation == null) return NotFound();
            return Ok(designation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDesignation(Designation designation)
        {
            _context.Designations.Add(designation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDesignation), new { id = designation.DesigID }, designation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDesignation(int id, Designation designation)
        {
            if (id != designation.DesigID) return BadRequest();
            _context.Entry(designation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("employee-count")]
        public async Task<IActionResult> GetDesignationEmployeeCount(int comId)
        {
            var counts = await _context.Employees
                .Where(e => e.ComID == comId)
                .GroupBy(e => e.Designation.DesigName)
                .Select(g => new
                {
                    Designation = g.Key,
                    EmployeeCount = g.Count()
                })
                .ToListAsync();

            return Ok(counts);
        }
    }
}