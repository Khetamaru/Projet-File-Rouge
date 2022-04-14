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
    public class RedWireController : ControllerBase
    {
        private readonly RedWireContext _context;

        public RedWireController(RedWireContext context) { _context = context; }

        private bool RedWireExists(long id) { return _context.RedWire.Any(e => e.id == id); }

        // GET: api/RedWire
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetRedWire()
        {
            return await _context.RedWire.ToListAsync();
        }

        // GET: api/RedWire/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RedWire>> GetRedWire(int id)
        {
            var RedWire = await _context.RedWire.FindAsync(id);
            if (RedWire == null)
            {
                return NotFound();
            }
            return RedWire;
        }

        // POST: api/RedWire
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RedWire>> PostRedWire(RedWire RedWire)
        {
            _context.RedWire.Add(RedWire);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRedWire), new { id = RedWire.id }, RedWire);
        }

        // PUT: api/RedWire/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRedWire(int id, RedWire RedWire)
        {
            if (id != RedWire.id)
            {
                return BadRequest();
            }
            _context.Entry(RedWire).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RedWireExists(id))
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

        // DELETE: api/RedWire/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRedWire(int id)
        {
            var RedWire = await _context.RedWire.FindAsync(id);
            if (RedWire == null)
            {
                return NotFound();
            }
            _context.RedWire.Remove(RedWire);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
