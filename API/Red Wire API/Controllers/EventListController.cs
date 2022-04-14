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
    public class EventListController : ControllerBase
    {
        private readonly EventListContext _context;

        public EventListController(EventListContext context) { _context = context; }

        private bool EventListExists(long id) { return _context.EventList.Any(e => e.id == id); }

        // GET: api/EventList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventList>>> GetEventList()
        {
            return await _context.EventList.ToListAsync();
        }

        // GET: api/EventList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventList>> GetEventList(int id)
        {
            var EventList = await _context.EventList.FindAsync(id);
            if (EventList == null)
            {
                return NotFound();
            }
            return EventList;
        }

        // POST: api/EventList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventList>> PostEventList(EventList EventList)
        {
            _context.EventList.Add(EventList);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEventList), new { id = EventList.id }, EventList);
        }

        // PUT: api/EventList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventList(int id, EventList EventList)
        {
            if (id != EventList.id)
            {
                return BadRequest();
            }
            _context.Entry(EventList).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventListExists(id))
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

        // DELETE: api/EventList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventList(int id)
        {
            var EventList = await _context.EventList.FindAsync(id);
            if (EventList == null)
            {
                return NotFound();
            }
            _context.EventList.Remove(EventList);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
