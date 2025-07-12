using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.AuthFolder;
using StudentService.Data;
using StudentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentService.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserCredentialsController : ControllerBase
    {
        private readonly StudentServiceContext _context;
        private readonly IAuth jwtAuth;
        public UserCredentialsController(StudentServiceContext context, IAuth jwtAuth)
        {
            _context = context;
            this.jwtAuth = jwtAuth;
        }

        // GET: api/UserCredentials
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCredentials>>> GetUserCredentials()
        {
            return await _context.UserCredentials.ToListAsync();
        }

        // GET: api/UserCredentials/5
       
        [HttpGet("{id}")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<UserCredentials>> GetUserCredentials(string id)
        {
            var userCredentials = await _context.UserCredentials.FindAsync(id);

            if (userCredentials == null)
            {
                return NotFound();
            }

            return userCredentials;
        }

        [AllowAnonymous]
        // POST api/<UserController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredentials userCredential)
        {
            var userInDb = _context.UserCredentials.FirstOrDefault(u => u.UserName == userCredential.UserName);
            if (userInDb == null)
                return Unauthorized();

            var result = _passwordHasher.VerifyHashedPassword(userInDb, userInDb.Password, userCredential.Password);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized();
            var generatedJwtToken = jwtAuth.Authentication(userCredential);
            if (generatedJwtToken == null)
                return Unauthorized();
            return Ok(new { token = generatedJwtToken });
        }
        // PUT: api/UserCredentials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> PutUserCredentials(string id, UserCredentials userCredentials)
        {
            if (id != userCredentials.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userCredentials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCredentialsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private readonly PasswordHasher<UserCredentials> _passwordHasher = new();
        // POST: api/UserCredentials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserCredentials>> PostUserCredentials(UserCredentials userCredentials)
        {
           
            userCredentials.Password = _passwordHasher.HashPassword(userCredentials, userCredentials.Password);
            _context.UserCredentials.Add(userCredentials);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserCredentialsExists(userCredentials.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserCredentials", new { id = userCredentials.UserName }, userCredentials);
        }

        // DELETE: api/UserCredentials/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserCredentials(string id)
        {
            var userCredentials = await _context.UserCredentials.FindAsync(id);
            if (userCredentials == null)
            {
                return NotFound();
            }

            _context.UserCredentials.Remove(userCredentials);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Authorize()]
        private bool UserCredentialsExists(string id)
        {
            return _context.UserCredentials.Any(e => e.UserName == id);
        }
    }
}
