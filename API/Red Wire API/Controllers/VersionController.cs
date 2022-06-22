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
    public class VersionController : ControllerBase
    {
        private readonly VersionContext _context;

        public VersionController(VersionContext context) { _context = context; }

        private bool VersionExists(long id) { return _context.Version.Any(e => e.id == id); }

        // GET: api/Version/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Version>> GetVersion(int id)
        {
            var Version = await _context.Version.FindAsync(id);
            if (Version == null)
            {
                return NotFound();
            }
            return Version;
        }

        // GET: api/Version/5
        [HttpGet("last")]
        public async Task<ActionResult<Version>> GetLastVersion()
        {
            Version Version = (await _context.Version.Where(v => v.versionNumber == _context.Version.Max(ve => ve.versionNumber)).ToListAsync()).First();
            if (Version == null)
            {
                return NotFound();
            }
            return Version;
        }

        // POST: api/Version
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Version>> PostVersion(Version Version)
        {
            _context.Version.Add(Version);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVersion), new { id = Version.id }, Version);
        }

        // PUT: api/Version/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVersion(int id, Version Version)
        {
            if (id != Version.id)
            {
                return BadRequest();
            }
            _context.Entry(Version).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VersionExists(id))
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

        // DELETE: api/Version/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVersion(int id)
        {
            var Version = await _context.Version.FindAsync(id);
            if (Version == null)
            {
                return NotFound();
            }
            _context.Version.Remove(Version);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
