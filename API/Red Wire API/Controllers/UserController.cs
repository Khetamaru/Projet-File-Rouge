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
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context) { _context = context; }

        private bool UserExists(long id) { return _context.User.Any(e => e.id == id); }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.OrderBy(u => u.accessLevel).ToListAsync();
        }

        // GET: api/User/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<User>>> GetActiveUser()
        {
            return await _context.User.OrderBy(u => u.accessLevel).Where(u => u.activated == true).ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return User;
        }

        // GET: api/User/Name/didier
        [HttpGet("Name/{name}")]
        public async Task<ActionResult<User>> GetUser(string name)
        {
            var User = await _context.User.Where(r => r.name == name).ToListAsync();
            if (User.Count <= 0)
            {
                return NotFound();
            }
            return User[0];
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInRequest(User user)
        {
            var _user = await _context.User.Where(r => r.name == user.name && r.password == user.password).ToListAsync();

            if (_user.Count <= 0)
            {
                return NotFound();
            }
            return Ok();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            _context.User.Add(User);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = User.id }, User);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.id)
            {
                return BadRequest();
            }
            _context.Entry(User).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            _context.User.Remove(User);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
