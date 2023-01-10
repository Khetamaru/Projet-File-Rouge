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
    public class DocumentFTPController : ControllerBase
    {
        private readonly DocumentFTPContext _context;

        public DocumentFTPController(DocumentFTPContext context) { _context = context; }

        private bool DocumentFTPExists(long id) { return _context.DocumentFTP.Any(e => e.id == id); }

        // GET: api/DocumentFTP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentFTP>>> GetDocumentFTP()
        {
            return await _context.DocumentFTP
                .Include(d => d.redWire)
                .Include(d => d.redWire.recorder)
                .ToListAsync();
        }

        // GET: api/DocumentFTP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentFTP>> GetDocumentFTP(int id)
        {
            var DocumentFTP = await _context.DocumentFTP
                .Where(d => d.id == id)
                .Include(d => d.redWire)
                .Include(d => d.redWire.recorder)
                .FirstOrDefaultAsync();

            if (DocumentFTP == null)
            {
                return NotFound();
            }
            return DocumentFTP;
        }

        // GET: api/Event/redwire/5
        [HttpGet("redwire/{id}")]
        public async Task<ActionResult<IEnumerable<DocumentFTP>>> GetDocumentFTPByRedWire(int id)
        {
            return await _context.DocumentFTP
                .Where(d => d.redWire.id == id)
                .Include(d => d.redWire)
                .Include(d => d.redWire.recorder)
                .OrderBy(d => d.id)
                .ToListAsync();
        }

        // POST: api/DocumentFTP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentFTP>> PostDocumentFTP(DocumentFTP DocumentFTP)
        {
            _context.DocumentFTP.Add(DocumentFTP);
            DocumentFTP.PostChild(_context);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDocumentFTP), new { id = DocumentFTP.id }, DocumentFTP);
        }

        // PUT: api/DocumentFTP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentFTP(int id, DocumentFTP DocumentFTP)
        {
            if (id != DocumentFTP.id)
            {
                return BadRequest();
            }
            _context.Entry(DocumentFTP).State = EntityState.Modified;
            _context.Entry(DocumentFTP.redWire).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentFTPExists(id))
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

        // DELETE: api/DocumentFTP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentFTP(int id)
        {
            var DocumentFTP = await _context.DocumentFTP.FindAsync(id);
            if (DocumentFTP == null)
            {
                return NotFound();
            }
            _context.DocumentFTP.Remove(DocumentFTP);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/DocumentFTP/redWire/5
        [HttpDelete("redWire/{id}")]
        public async Task<IActionResult> DeleteDocumentFTPByRedWire(int id)
        {
            var DocumentFTP = await _context.DocumentFTP.Where(r => r.redWire.id == id).ToListAsync();
            foreach (DocumentFTP documentFTP in DocumentFTP)
            {
                _context.DocumentFTP.Remove(documentFTP);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
