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

        // GET: api/RedWire/page/0/0001-01-01T00:00:00/-1/-1/CM
        [HttpGet("page/{pageNumber}/{date}/{step}/{userId}/{clientName}")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetRedWireFiltered(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire.OrderByDescending(r => r.inChargeDate);

            result = Filtering(result, date, step, userId, clientName);

            result = result.Skip(pageNumber * 10).Take(10);

            return await result.ToListAsync();
        }

        // GET: api/RedWire/total/0001-01-01T00:00:00/-1/-1/CM
        [HttpGet("total/{date}/{step}/{userId}/{clientName}")]
        public ActionResult<int> GetRedWireNumber(DateTime date, int step, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire;

            result = Filtering(result, date, step, userId, clientName);

            return result.Count();
        }

        private static IQueryable<RedWire> Filtering(IQueryable<RedWire> result, DateTime date, int step, int userId, string clientName)
        {
            if (date != new DateTime()) { result = result.Where(r => r.inChargeDate.Day == date.Day); }

            if (step >= 0) { result = result.Where(r => r.actualState == step); }

            if (userId >= 0) { result = result.Where(r => r.actualRepairMan == userId); }

            if (clientName != null && clientName != string.Empty && clientName != "*") { result = result.Where(r => r.clientName.Contains(clientName)); }

            return result;
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

        // GET: api/RedWire/notif/0
        [HttpGet("notif/{userId}")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetRedWireNotif(int userId)
        {
            IQueryable<RedWire> result = _context.RedWire;
            result = Filter(result, userId);
            return await result.ToListAsync();

        }

        // GET: api/RedWire/notifNumber/0
        [HttpGet("notifNumber/{userId}")]
        public ActionResult<int> GetRedWireNotifNumber(int userId)
        {
            IQueryable<RedWire> result = _context.RedWire;
            result = Filter(result, userId);
            return result.Count();
        }

        public static IQueryable<RedWire> Filter(IQueryable<RedWire> result, int userId)
        {
            result = result.Where(r => (r.actualState != 0
                                     && r.actualState != 5
                                     && r.actualState != 6
                                     && r.actualState != 10
                                     && r.actualState != 11
                                     && r.actualState != 13
                                    && (r.actualRepairMan == userId 
                                    || (r.actualState == 7 
                                     && r.transfertTarget == userId))
                                     && r.lastUpdate <= DateTime.Now.AddDays(-7))
                                    || (r.repairStartDate <= DateTime.Now.AddMonths(-6)
                                     && r.actualState != 11));

            return result;
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
