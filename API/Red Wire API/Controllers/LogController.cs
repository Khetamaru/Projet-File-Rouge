using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local_API_Server.Models;
using Projet_File_Rouge.Object;
using System;

namespace Local_API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly LogContext _context;

        public LogController(LogContext context) { _context = context; }

        private bool LogExists(long id) { return _context.Log.Any(e => e.id == id); }

        // GET: api/Log
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLog()
        {
            return await _context.Log
                .Include(l => l.user)
                .ToListAsync();
        }

        // GET: api/Log/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLog(int id)
        {
            var Log = await _context.Log
                .Where(l => l.id == id)
                .Include(l => l.user)
                .FirstOrDefaultAsync();

            if (Log == null)
            {
                return NotFound();
            }
            return Log;
        }

        // GET: api/Log/page/0/0001-01-01T00:00:00/-1/-1
        [HttpGet("page/{pageNumber}/{date}/{type}/{userId}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogFiltered(int pageNumber, DateTime date, int type, int userId)
        {
            IQueryable<Log> result = _context.Log.OrderByDescending(l => l.date); ;

            result = Filtering(result, date, type, userId);

            result = result
                .Include(l => l.user)
                .Skip(pageNumber * 10)
                .Take(10);

            return await result.ToListAsync();
        }

        // GET: api/Log/total/0001-01-01T00:00:00/-1/-1
        [HttpGet("total/{date}/{type}/{userId}")]
        public ActionResult<int> GetLogNumber(DateTime date, int type, int userId)
        {
            IQueryable<Log> result = _context.Log;

            result = Filtering(result, date, type, userId);

            return result.Count();
        }

        private static IQueryable<Log> Filtering(IQueryable<Log> result, DateTime date, int type, int userId)
        {
            if (date != new DateTime()) { result = result.Where(l => l.date.Day == date.Day && l.date.Month == date.Month && l.date.Year == date.Year); }

            if (type >= 0) { result = result.Where(l => l.type == type); }

            if (userId >= 0) { result = result.Where(l => l.user.id == userId); }

            return result;
        }

        // POST: api/Log
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Log>> PostLog(Log Log)
        {
            _context.Log.Add(Log);
            Log.PostChild(_context);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLog), new { id = Log.id }, Log);
        }

        // PUT: api/Log/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLog(int id, Log Log)
        {
            if (id != Log.id)
            {
                return BadRequest();
            }
            _context.Entry(Log).State = EntityState.Modified;
            _context.Entry(Log.user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id))
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

        // DELETE: api/Log/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var Log = await _context.Log.FindAsync(id);
            if (Log == null)
            {
                return NotFound();
            }
            _context.Log.Remove(Log);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
