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
    public class MissingCallController : ControllerBase
    {
        private readonly MissingCallContext _context;

        public MissingCallController(MissingCallContext context) { _context = context; }

        private bool MissingCallExists(long id) { return _context.MissingCall.Any(e => e.id == id); }

        // GET: api/MissingCall
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissingCall>>> GetMissingCall()
        {
            return await _context.MissingCall.ToListAsync();
        }

        // GET: api/MissingCall/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MissingCall>> GetMissingCall(int id)
        {
            var MissingCall = await _context.MissingCall
                .Where(m => m.id == id)
                .Include(m => m.author)
                .Include(m => m.recipient)
                .FirstOrDefaultAsync();

            if (MissingCall == null)
            {
                return NotFound();
            }
            return MissingCall;
        }

        // GET: api/MissingCall/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<MissingCall>>> GetMissingCallByUser(int id)
        {
            var MissingCall = await _context.MissingCall
                .Where(m => m.recipient.id == id)
                .Include(m => m.author)
                .Include(m => m.recipient)
                .OrderByDescending(m => m.id)
                .ToListAsync();

            if (MissingCall == null)
            {
                return NotFound();
            }
            return MissingCall;
        }

        // GET: api/MissingCall/5
        [HttpGet("user/send/{id}")]
        public async Task<ActionResult<IEnumerable<MissingCall>>> GetMissingCallByUserSend(int id)
        {
            var MissingCall = await _context.MissingCall
                .Where(m => m.author.id == id)
                .Include(m => m.author)
                .Include(m => m.recipient)
                .OrderByDescending(m => m.id)
                .ToListAsync();

            if (MissingCall == null)
            {
                return NotFound();
            }
            return MissingCall;
        }

        // GET: api/MissingCall/unreadNumber/5
        [HttpGet("unreadNumber/{id}")]
        public async Task<ActionResult<int>> GetMissingCallUnreadNumber(int id)
        {
            var MissingCall = await _context.MissingCall
                .Where(m => m.recipient.id == id && m.read == false)
                .Include(m => m.author)
                .Include(m => m.recipient)
                .OrderByDescending(m => m.id)
                .ToListAsync();

            if (MissingCall == null)
            {
                return NotFound();
            }
            return MissingCall.Count();
        }

        // GET: api/MissingCall/unread/5
        [HttpGet("unread/{id}")]
        public async Task<ActionResult<IEnumerable<MissingCall>>> GetMissingCallUnread(int id)
        {
            var MissingCall = await _context.MissingCall
                .Where(m => m.recipient.id == id && m.read == false)
                .Include(m => m.author)
                .Include(m => m.recipient)
                .OrderByDescending(m => m.id)
                .ToListAsync();

            if (MissingCall == null)
            {
                return NotFound();
            }
            return MissingCall;
        }

        // POST: api/MissingCall
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MissingCall>> PostMissingCall(MissingCall MissingCall)
        {
            _context.MissingCall.Add(MissingCall);
            MissingCall.PostChild(_context);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMissingCall), new { id = MissingCall.id }, MissingCall);
        }

        // PUT: api/MissingCall/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMissingCall(int id, MissingCall MissingCall)
        {
            if (id != MissingCall.id)
            {
                return BadRequest();
            }

            await DispatchPut(id, MissingCall);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissingCallExists(id))
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

        private async Task<IActionResult> DispatchPut(int id, MissingCall MissingCall)
        {
            MissingCall bddMissingCall = await _context.MissingCall
                .Where(r => r.id == id)
                .Include(r => r.author)
                .Include(r => r.recipient)
                .FirstOrDefaultAsync();

            _context.ChangeTracker.Clear();

            _context.Entry(MissingCall).State = EntityState.Modified;

            if (bddMissingCall != null)
            {
                if ((bddMissingCall.author != null && MissingCall.author == null)
                 || (bddMissingCall.author == null && MissingCall.author != null)
                 || (bddMissingCall.author != null && MissingCall.author != null && bddMissingCall.author.id != MissingCall.author.id))
                {
                    _context.Entry(MissingCall.author).State = EntityState.Modified;
                    return Ok();
                }
                if ((bddMissingCall.recipient != null && MissingCall.recipient == null)
                 || (bddMissingCall.recipient == null && MissingCall.recipient != null)
                 || (bddMissingCall.recipient != null && MissingCall.recipient != null && bddMissingCall.recipient.id != MissingCall.recipient.id))
                {
                    _context.Entry(MissingCall.recipient).State = EntityState.Modified;
                    return Ok();
                }
            }
            return NotFound();
        }

        // PUT: api/MissingCall/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("author/{id}")]
        public async Task<IActionResult> PutMissingCallAuthor(int id, MissingCall MissingCall)
        {
            _context.Entry(MissingCall.author).State = EntityState.Modified;

            return await PutMissingCall(id, MissingCall);
        }

        // PUT: api/MissingCall/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("recipient/{id}")]
        public async Task<IActionResult> PutMissingCallRecipient(int id, MissingCall MissingCall)
        {
            _context.Entry(MissingCall.recipient).State = EntityState.Modified;

            return await PutMissingCall(id, MissingCall);
        }

        // DELETE: api/MissingCall/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMissingCall(int id)
        {
            var MissingCall = await _context.MissingCall.FindAsync(id);
            if (MissingCall == null)
            {
                return NotFound();
            }
            _context.MissingCall.Remove(MissingCall);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
