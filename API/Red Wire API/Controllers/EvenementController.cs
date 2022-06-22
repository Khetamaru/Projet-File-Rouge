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
    public class EvenementController : ControllerBase
    {
        private readonly EvenementContext _context;

        public EvenementController(EvenementContext context) { _context = context; }

        private bool EventExists(long id) { return _context.Evenement.Any(e => e.id == id); }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evenement>>> GetEvent()
        {
            return await _context.Evenement.ToListAsync();
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evenement>> GetEvent(int id)
        {
            var Event = await _context.Evenement.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }
            return Event;
        }

        // GET: api/Event/redwire/5
        [HttpGet("redwire/{id}")]
        public async Task<ActionResult<IEnumerable<Evenement>>> GetEventByRedWire(int id)
        {
            return await _context.Evenement.Where(e => e.redWire == id).OrderByDescending(e => e.id).ToListAsync();
        }

        // POST: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evenement>> PostEvent(Evenement Event)
        {
            _context.Evenement.Add(Event);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new { id = Event.id }, Event);
        }

        // PUT: api/Event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Evenement Event)
        {
            if (id != Event.id)
            {
                return BadRequest();
            }
            _context.Entry(Event).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // DELETE: api/Event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var Event = await _context.Evenement.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }
            _context.Evenement.Remove(Event);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Event/redWire/5
        [HttpDelete("redWire/{id}")]
        public async Task<IActionResult> DeleteEventByRedWire(int id)
        {
            var Event = await _context.Evenement.Where(r => r.redWire == id).ToListAsync();
            foreach (Evenement evenement in Event)
            {
                _context.Evenement.Remove(evenement);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
