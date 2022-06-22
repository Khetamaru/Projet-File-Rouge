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
    public class UserHistoryListController : ControllerBase
    {
        private readonly UserHistoryListContext _context;

        public UserHistoryListController(UserHistoryListContext context) { _context = context; }

        private bool UserHistoryListExists(long id) { return _context.UserHistoryList.Any(e => e.id == id); }

        // GET: api/UserHistoryList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserHistoryList>>> GetUserHistoryList()
        {
            return await _context.UserHistoryList.ToListAsync();
        }

        // GET: api/UserHistoryList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserHistoryList>> GetUserHistoryList(int id)
        {
            var UserHistoryList = await _context.UserHistoryList.FindAsync(id);
            if (UserHistoryList == null)
            {
                return NotFound();
            }
            return UserHistoryList;
        }

        // GET: api/UserHistoryList/redWire/5
        [HttpGet("redWire/{id}")]
        public async Task<ActionResult<IEnumerable<UserHistoryList>>> GetUserHistoryListByRedWire(int id)
        {
            var UserHistoryList = await _context.UserHistoryList.Where(u => u.redWire == id).ToListAsync();

            return UserHistoryList;
        }

        // POST: api/UserHistoryList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserHistoryList>> PostUserHistoryList(UserHistoryList UserHistoryList)
        {
            _context.UserHistoryList.Add(UserHistoryList);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserHistoryList), new { id = UserHistoryList.id }, UserHistoryList);
        }

        // PUT: api/UserHistoryList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserHistoryList(int id, UserHistoryList UserHistoryList)
        {
            if (id != UserHistoryList.id)
            {
                return BadRequest();
            }
            _context.Entry(UserHistoryList).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserHistoryListExists(id))
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

        // DELETE: api/UserHistoryList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserHistoryList(int id)
        {
            var UserHistoryList = await _context.UserHistoryList.FindAsync(id);
            if (UserHistoryList == null)
            {
                return NotFound();
            }
            _context.UserHistoryList.Remove(UserHistoryList);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/UserHistoryList/redWire/5
        [HttpDelete("redWire/{id}")]
        public async Task<IActionResult> DeleteUserHistoryListByRedWire(int id)
        {
            var UserHistoryList = await _context.UserHistoryList.Where(r => r.redWire == id).ToListAsync();
            foreach(UserHistoryList userHistoryList in UserHistoryList)
            {
                _context.UserHistoryList.Remove(userHistoryList);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
