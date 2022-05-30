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
    public class CommandListController : ControllerBase
    {
        private readonly CommandListContext _context;

        public CommandListController(CommandListContext context) { _context = context; }

        private bool CommandListExists(long id) { return _context.CommandList.Any(e => e.id == id); }

        // GET: api/CommandList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandList>>> GetCommandList()
        {
            return await _context.CommandList.ToListAsync();
        }

        // GET: api/CommandList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommandList>> GetCommandList(int id)
        {
            var CommandList = await _context.CommandList.FindAsync(id);
            if (CommandList == null)
            {
                return NotFound();
            }
            return CommandList;
        }

        // GET: api/CommandList/redwire/5
        [HttpGet("redwire/{id}")]
        public async Task<ActionResult<IEnumerable<CommandList>>> GetCommandListByRedWire(int id)
        {
            return await _context.CommandList.Where(c => c.redWire == id).OrderBy(d => d.id).ToListAsync();
        }

        // GET: api/CommandList/number
        [HttpGet("number")]
        public ActionResult<int> GetCommandListNumber()
        {
            return Filter(_context.CommandList).Count();
        }

        // GET: api/CommandList/page
        [HttpGet("page/{pageNumber}")]
        public async Task<ActionResult<IEnumerable<CommandList>>> GetCommandListPage(int pageNumber)
        {
            return await Filter(_context.CommandList).Skip(pageNumber * 10).Take(10).ToListAsync();
        }

        public static IQueryable<CommandList> Filter(IQueryable<CommandList> result)
        {
            result = result.OrderBy(c => c.state);

            return result;
        }

        // GET: api/CommandList/notif
        [HttpGet("notif")]
        public async Task<ActionResult<IEnumerable<CommandList>>> GetCommandListNotif()
        {
            return await Filter().ToListAsync();
        }

        // GET: api/CommandList/notifNumber
        [HttpGet("notifNumber")]
        public ActionResult<int> GetCommandListNotifNumber()
        {
            return Filter().Count();
        }

        public IQueryable<CommandList> Filter()
        {
            return _context.CommandList.Where(c => (c.deliveryDate.Date < DateTime.Now
                                                && c.state == 1)
                                                || c.state == 0);
        }

        // POST: api/CommandList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommandList>> PostCommandList(CommandList CommandList)
        {
            _context.CommandList.Add(CommandList);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCommandList), new { id = CommandList.id }, CommandList);
        }

        // PUT: api/CommandList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommandList(int id, CommandList CommandList)
        {
            if (id != CommandList.id)
            {
                return BadRequest();
            }
            _context.Entry(CommandList).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandListExists(id))
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

        // DELETE: api/CommandList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommandList(int id)
        {
            var CommandList = await _context.CommandList.FindAsync(id);
            if (CommandList == null)
            {
                return NotFound();
            }
            _context.CommandList.Remove(CommandList);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
