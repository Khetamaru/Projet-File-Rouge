using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local_API_Server.Models;
using Projet_File_Rouge.Object;
using System;
using Red_Wire_API.Models;

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
            return await _context.RedWire
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .ToListAsync();
        }

        // GET: api/RedWire/page/0/0001-01-01T00:00:00/-1/-1/CM
        [HttpGet("page/{pageNumber}/{date}/{step}/{userId}/{clientName}")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetRedWireFiltered(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .OrderByDescending(r => r.inChargeDate);

            result = Filtering(result, date, step, userId, clientName);

            result = result.Skip(pageNumber * 10).Take(10);

            return await result.ToListAsync();
        }

        // GET: api/RedWire/page/0/0001-01-01T00:00:00/-1/-1/CM
        [HttpGet("page/old/{pageNumber}/{date}/{userId}/{clientName}")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetOldRedWireFiltered(int pageNumber, DateTime date, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .OrderByDescending(r => r.inChargeDate);

            result = Filtering(result, date, userId, clientName);

            result = result.Skip(pageNumber * 10).Take(10);

            return await result.ToListAsync();
        }

        // GET: api/RedWire/page/0/0001-01-01T00:00:00/-1/-1/CM
        [HttpGet("page/return/{pageNumber}/{date}/{userId}/{clientName}")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetReturnRedWireFiltered(int pageNumber, DateTime date, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .OrderByDescending(r => r.inChargeDate);

            result = FilteringReturn(result, date, userId, clientName);

            result = result.Skip(pageNumber * 10).Take(10);

            return await result.ToListAsync();
        }

        // GET: api/RedWire/page/0/0001-01-01T00:00:00/-1/CM
        [HttpGet("page/noOld/{pageNumber}/{date}/{step}/{userId}/{clientName}")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetRedWireFilteredNoOld(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .OrderByDescending(r => r.inChargeDate);

            result = Filtering(result, date, step, userId, clientName);

            result = result.Where(r => r.actualState != 10 && r.actualState != 13);

            result = result.Skip(pageNumber * 10).Take(10);

            return await result.ToListAsync();
        }

        // GET: api/RedWire/page/0/0001-01-01T00:00:00/-1/CM
        [HttpGet("page/Perso/{pageNumber}/{date}/{step}/{userId}/{clientName}")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetRedWireFilteredPerso(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .OrderByDescending(r => r.inChargeDate);

            result = FilteringPerso(result, date, step, userId, clientName);

            result = result.Where(r => r.actualState != 10 && r.actualState != 13);

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

        // GET: api/RedWire/total/0001-01-01T00:00:00/-1/CM
        [HttpGet("total/old/{date}/{userId}/{clientName}")]
        public ActionResult<int> GetRedWireNumber(DateTime date, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire;

            result = Filtering(result, date, userId, clientName);

            return result.Count();
        }

        // GET: api/RedWire/total/0001-01-01T00:00:00/-1/CM
        [HttpGet("total/return/{date}/{userId}/{clientName}")]
        public ActionResult<int> GetReturnRedWireNumber(DateTime date, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire;

            result = FilteringReturn(result, date, userId, clientName);

            return result.Count();
        }

        // GET: api/RedWire/total/0001-01-01T00:00:00/-1/-1/CM
        [HttpGet("total/noOld/{date}/{step}/{userId}/{clientName}")]
        public ActionResult<int> GetRedWireNumberNoOld(DateTime date, int step, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire;

            result = Filtering(result, date, step, userId, clientName);

            result = result.Where(r => r.actualState != 10 && r.actualState != 13);

            return result.Count();
        }

        // GET: api/RedWire/total/0001-01-01T00:00:00/-1/-1/CM
        [HttpGet("total/Perso/{date}/{step}/{userId}/{clientName}")]
        public ActionResult<int> GetRedWireNumberPerso(DateTime date, int step, int userId, string clientName)
        {
            IQueryable<RedWire> result = _context.RedWire;

            result = FilteringPerso(result, date, step, userId, clientName);

            result = result.Where(r => r.actualState != 10 && r.actualState != 13);

            return result.Count();
        }

        private static IQueryable<RedWire> Filtering(IQueryable<RedWire> result, DateTime date, int step, int userId, string clientName)
        {
            if (date != new DateTime()) { result = result.Where(r => r.inChargeDate.Day == date.Day); }

            if (step >= 0) { result = result.Where(r => r.actualState == step); }

            if (userId >= 0) { result = result.Where(r => r.actualRepairMan.id == userId || (r.transfertTarget.id == userId && r.actualState == 7)); }

            if (clientName != null && clientName != string.Empty && clientName != "*") { result = result.Where(r => r.clientName.Contains(clientName)); }

            return result;
        }

        private static IQueryable<RedWire> FilteringPerso(IQueryable<RedWire> result, DateTime date, int step, int userId, string clientName)
        {
            if (date != new DateTime()) { result = result.Where(r => r.inChargeDate.Day == date.Day); }

            if (step >= 0) { result = result.Where(r => r.actualState == step); }

            if (userId >= 0) { result = result.Where(r => r.actualRepairMan.id == userId || (r.transfertTarget.id == userId && r.actualState == 7)); }

            if (clientName != null && clientName != string.Empty && clientName != "*") { result = result.Where(r => r.clientName.Contains(clientName)); }

            result = result.Where(r => r.actualState != 9 && r.actualState != 12);

            return result;
        }

        private static IQueryable<RedWire> Filtering(IQueryable<RedWire> result, DateTime date, int userId, string clientName)
        {
            if (date != new DateTime()) { result = result.Where(r => r.inChargeDate.Day == date.Day); }

            result = result.Where(r => r.actualState == 10 || r.actualState == 13);

            if (userId >= 0) { result = result.Where(r => r.actualRepairMan.id == userId || (r.transfertTarget.id == userId && r.actualState == 7)); }

            if (clientName != null && clientName != string.Empty && clientName != "*") { result = result.Where(r => r.clientName.Contains(clientName)); }

            return result;
        }

        private static IQueryable<RedWire> FilteringReturn(IQueryable<RedWire> result, DateTime date, int userId, string clientName)
        {
            if (date != new DateTime()) { result = result.Where(r => r.inChargeDate.Day == date.Day); }

            result = result.Where(r => r.actualState == 9 || r.actualState == 12);

            if (userId >= 0) { result = result.Where(r => r.actualRepairMan.id == userId); }

            if (clientName != null && clientName != string.Empty && clientName != "*") { result = result.Where(r => r.clientName.Contains(clientName)); }

            return result;
        }

        // GET: api/RedWire/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RedWire>> GetRedWire(int id)
        {
            var RedWire = await _context.RedWire
                .Where(c => c.id == id)
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .FirstOrDefaultAsync();

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
            return await result
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .ToListAsync();

        }

        // GET: api/RedWire/notifAdmin
        [HttpGet("notifAdmin")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetRedWireNotifAdmin()
        {
            IQueryable<RedWire> result = _context.RedWire;
            result = FilterAdmin(result);
            return await result
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .ToListAsync();
        }

        // GET: api/RedWire/notifNumber/0
        [HttpGet("notifNumber/{userId}")]
        public ActionResult<int> GetRedWireNotifNumber(int userId)
        {
            IQueryable<RedWire> result = _context.RedWire;
            result = Filter(result, userId);
            return result.Count();
        }

        // GET: api/RedWire/notifAdminNumber
        [HttpGet("notifAdminNumber")]
        public ActionResult<int> GetRedWireNotifAdminNumber()
        {
            IQueryable<RedWire> result = _context.RedWire;
            result = FilterAdmin(result);
            return result.Count();
        }

        // GET: api/RedWire/notifAdminNumber
        [HttpGet("notifPrividerWaitingNumber")]
        public ActionResult<int> GetRedWireNotifPrividerWaitingNumber()
        {
            IQueryable<RedWire> result = _context.RedWire;
            result = FilterPrividerWaiting(result);
            return result.Count();
        }

        // GET: api/RedWire/purgeNumber/2022-05-01T00:00:00
        [HttpGet("purgeNumber")]
        public ActionResult<int> GetRedWirePurgeNumber()
        {
            IQueryable<RedWire> result = _context.RedWire;
            result = PurgeFilter(result);
            return result.Count();
        }

        // GET: api/RedWire/purge/2022-05-01T00:00:00
        [HttpGet("purge")]
        public async Task<ActionResult<IEnumerable<RedWire>>> GetRedWirePurge()
        {
            IQueryable<RedWire> result = _context.RedWire;
            result = PurgeFilter(result);
            return await result
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .ToListAsync();
        }

        private IQueryable<RedWire> PurgeFilter(IQueryable<RedWire> result)
        {
            DateTime purgeDate = DateTime.Now.AddYears(-3);
            DateTime startPurgeDate = Convert.ToDateTime(purgeDate.Year + "-08-31T00:00:00");

            if (purgeDate < startPurgeDate)
            {
                startPurgeDate = startPurgeDate.AddYears(-1);
            }

            result = result.Where(r => r.actualState == 10
                                    && r.lastUpdate <= startPurgeDate);

            return result;
        }

        public static IQueryable<RedWire> Filter(IQueryable<RedWire> result, int userId)
        {
            result = result.Where(r => (
                                               r.actualState != 0
                                            && r.actualState != 5
                                            && r.actualState != 6
                                            && r.actualState != 9
                                            && r.actualState != 10
                                            && r.actualState != 12
                                            && r.actualState != 13

                                            &&
                                            (
                                                r.actualRepairMan.id == userId
                                                || (r.actualState == 7 && r.transfertTarget.id == userId)
                                            )
                                            && r.lastUpdate <= DateTime.Now.AddDays(-7)
                                       )

                                       || (
                                            r.repairStartDate <= DateTime.Now.AddMonths(-6)
                                            && r.actualState != 0
                                            && r.actualState != 10
                                            && r.actualState != 13
                                            && r.actualState != 15

                                            &&
                                            (
                                                r.actualRepairMan.id == userId
                                                || (r.actualState == 7 && r.transfertTarget.id == userId)
                                            )
                                          )

                                       || (
                                            r.lastUpdate <= DateTime.Now.AddDays(-2)
                                            && r.actualState == 14

                                            &&
                                            (
                                                r.actualRepairMan.id == userId
                                            )
                                          )
                                       || (r.actualState == 7

                                            &&
                                            (
                                                r.actualRepairMan.id == userId
                                                || r.transfertTarget.id == userId
                                            )
                                          )
                                       );

            return result;
        }

        public static IQueryable<RedWire> FilterAdmin(IQueryable<RedWire> result)
        {
            result = result.Where(r => (r.actualState != 0
                                     && r.actualState != 5
                                     && r.actualState != 6
                                     && r.actualState != 9
                                     && r.actualState != 10
                                     && r.actualState != 12
                                     && r.actualState != 13
                                     && r.lastUpdate <= DateTime.Now.AddDays(-7).AddDays(-3))
                                    || (r.repairStartDate <= DateTime.Now.AddMonths(-6).AddDays(-3)
                                     && r.actualState != 0
                                     && r.actualState != 10
                                     && r.actualState != 13)
                                    || (r.actualState == 14
                                     && r.lastUpdate <= DateTime.Now.AddDays(-2).AddDays(-3))
                                    || r.actualState == 7);

            return result;
        }

        public static IQueryable<RedWire> FilterPrividerWaiting(IQueryable<RedWire> result)
        {
            result = result.Where(r => r.actualState == 14
                                     && r.lastUpdate <= DateTime.Now.AddDays(-2));

            return result;
        }

        // POST: api/RedWire
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RedWire>> PostRedWire(RedWire RedWire)
        {
            _context.RedWire.Add(RedWire);
            RedWire.PostChild(_context);
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
            await DispatchPut(id, RedWire);

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

        private async Task<IActionResult> DispatchPut(int id, RedWire RedWire)
        {
            RedWire bddRedWire = await _context.RedWire
                .Where(r => r.id == id)
                .Include(c => c.recorder)
                .Include(c => c.actualRepairMan)
                .Include(c => c.transfertTarget)
                .FirstOrDefaultAsync();

            _context.ChangeTracker.Clear();
            _context.Entry(RedWire).State = EntityState.Modified;

            if (bddRedWire != null)
            {
                if ((bddRedWire.recorder != null && RedWire.recorder == null)
                 || (bddRedWire.recorder == null && RedWire.recorder != null)
                 || (bddRedWire.recorder != null && RedWire.recorder != null && bddRedWire.recorder.id != RedWire.recorder.id))
                {
                    _context.Entry(RedWire.recorder).State = EntityState.Modified;
                    return Ok();
                }
                if ((bddRedWire.actualRepairMan != null && RedWire.actualRepairMan == null)
                 || (bddRedWire.actualRepairMan == null && RedWire.actualRepairMan != null)
                 || (bddRedWire.actualRepairMan != null && RedWire.actualRepairMan != null && bddRedWire.actualRepairMan.id != RedWire.actualRepairMan.id))
                {
                    _context.Entry(RedWire.actualRepairMan).State = EntityState.Modified;
                    return Ok();
                }
                if ((bddRedWire.transfertTarget != null && RedWire.transfertTarget == null)
                 || (bddRedWire.transfertTarget == null && RedWire.transfertTarget != null)
                 || (bddRedWire.transfertTarget != null && RedWire.transfertTarget != null && bddRedWire.transfertTarget.id != RedWire.transfertTarget.id))
                {
                    _context.Entry(RedWire.transfertTarget).State = EntityState.Modified;
                    return Ok();
                }
            }
            return NotFound();
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
