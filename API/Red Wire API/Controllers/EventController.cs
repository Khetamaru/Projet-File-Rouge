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
    public class EventController : ControllerBase
    {
        private readonly EventContext _context;

        public EventController(EventContext context) { _context = context; }

        private bool EventExists(long id) { return _context.Event.Any(e => e.id == id); }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            return await _context.Event.ToListAsync();
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var Event = await _context.Event.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }
            return Event;
        }

        // POST: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event Event)
        {
            _context.Event.Add(Event);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new { id = Event.id }, Event);
        }

        // PUT: api/Event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event Event)
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
            var Event = await _context.Event.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }
            _context.Event.Remove(Event);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
