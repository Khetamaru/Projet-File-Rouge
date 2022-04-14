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
            return await _context.DocumentList.ToListAsync();
        }

        // GET: api/DocumentList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentList>> GetDocumentList(int id)
        {
            var DocumentList = await _context.DocumentList.FindAsync(id);
            if (DocumentList == null)
            {
                return NotFound();
            }
            return DocumentList;
        }

        // POST: api/DocumentList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentList>> PostDocumentList(DocumentList DocumentList)
        {
            _context.DocumentList.Add(DocumentList);
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
    }
}
