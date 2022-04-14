using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local_API_Server.Models;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentContext _context;

        public EquipmentController(EquipmentContext context) { _context = context; }

        private bool EquipmentExists(long id) { return _context.Equipment.Any(e => e.id == id); }

        // GET: api/Equipment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipment()
        {
            return await _context.Equipment.ToListAsync();
        }

        // GET: api/Equipment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetEquipment(int id)
        {
            var Equipment = await _context.Equipment.FindAsync(id);
            if (Equipment == null)
            {
                return NotFound();
            }
            return Equipment;
        }

        // POST: api/Equipment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Equipment>> PostEquipment(Equipment Equipment)
        {
            _context.Equipment.Add(Equipment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEquipment), new { id = Equipment.id }, Equipment);
        }

        // PUT: api/Equipment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment(int id, Equipment Equipment)
        {
            if (id != Equipment.id)
            {
                return BadRequest();
            }
            _context.Entry(Equipment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        // DELETE: api/Equipment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var Equipment = await _context.Equipment.FindAsync(id);
            if (Equipment == null)
            {
                return NotFound();
            }
            _context.Equipment.Remove(Equipment);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
