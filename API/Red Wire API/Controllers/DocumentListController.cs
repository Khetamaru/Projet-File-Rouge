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
    public class DocumentListController : ControllerBase
    {
        private readonly DocumentListContext _context;

        public DocumentListController(DocumentListContext context) { _context = context; }

        private bool DocumentListExists(long id) { return _context.DocumentList.Any(e => e.id == id); }

        // GET: api/DocumentList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentList>>> GetDocumentList()
        {
            return await _context.DocumentList
                .Include(d => d.redWire)
                .Include(d => d.redWire.recorder)
                .ToListAsync();
        }

        // GET: api/DocumentList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentList>> GetDocumentList(int id)
        {
            var DocumentList = await _context.DocumentList
                .Where(d => d.id == id)
                .Include(d => d.redWire)
                .Include(d => d.redWire.recorder)
                .FirstOrDefaultAsync();

            if (DocumentList == null)
            {
                return NotFound();
            }
            return DocumentList;
        }

        // GET: api/Event/redwire/5
        [HttpGet("redwire/{id}")]
        public async Task<ActionResult<IEnumerable<DocumentList>>> GetDocumentListByRedWire(int id)
        {
            return await _context.DocumentList
                .Where(d => d.redWire.id == id)
                .Include(d => d.redWire)
                .Include(d => d.redWire.recorder)
                .OrderBy(d => d.id)
                .ToListAsync();
        }

        // POST: api/DocumentList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentList>> PostDocumentList(DocumentList DocumentList)
        {
            _context.DocumentList.Add(DocumentList);
            DocumentList.PostChild(_context);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDocumentList), new { id = DocumentList.id }, DocumentList);
        }

        // PUT: api/DocumentList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentList(int id, DocumentList DocumentList)
        {
            if (id != DocumentList.id)
            {
                return BadRequest();
            }
            _context.Entry(DocumentList).State = EntityState.Modified;
            _context.Entry(DocumentList.redWire).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentListExists(id))
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

        // DELETE: api/DocumentList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentList(int id)
        {
            var DocumentList = await _context.DocumentList.FindAsync(id);
            if (DocumentList == null)
            {
                return NotFound();
            }
            _context.DocumentList.Remove(DocumentList);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/DocumentList/redWire/5
        [HttpDelete("redWire/{id}")]
        public async Task<IActionResult> DeleteDocumentListByRedWire(int id)
        {
            var DocumentList = await _context.DocumentList.Where(r => r.redWire.id == id).ToListAsync();
            foreach (DocumentList documentList in DocumentList)
            {
                _context.DocumentList.Remove(documentList);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
