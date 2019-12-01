using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollAPI.Models;
using PollAPI.Services;

namespace PollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly PollContext _context;
        private IGebruikerService _gebruikerService;

        public GebruikerController(PollContext context, IGebruikerService gebruikerService)
        {
            _context = context;
            _gebruikerService = gebruikerService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Gebruiker userParam)
        {
            var gerbuiker = _gebruikerService.Authenticate(userParam.Gebruikersnaam, userParam.Wachtwoord);
            if (gerbuiker == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(gerbuiker);
        }

        // GET: api/Gebruiker
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gebruiker>>> GetGebruikers()
        {
            var gebruikerId = User.Claims.FirstOrDefault(c => c.Type == "GebruikerId").Value;
            return await _context.Gebruikers.ToListAsync();
        }

        [Authorize]
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Gebruiker>> GetPollGebruikersWhereGebruikerId(string email)
        {
            var gebruiker = await _context.Gebruikers.Where(g => g.Email == email).FirstOrDefaultAsync();

            if (gebruiker == null)
            {
                return NotFound();
            }

            return gebruiker;
        }

        // GET: api/Gebruiker/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Gebruiker>> GetGebruiker(int id)
        {
            var gebruiker = await _context.Gebruikers.FindAsync(id);

            if (gebruiker == null)
            {
                return NotFound();
            }

            return gebruiker;
        }

        // PUT: api/Gebruiker/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGebruiker(int id, Gebruiker gebruiker)
        {
            if (id != gebruiker.GebruikerId)
            {
                return BadRequest();
            }

            _context.Entry(gebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GebruikerExists(id))
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

        // POST: api/Gebruiker
        [HttpPost]
        public async Task<ActionResult<Gebruiker>> PostGebruiker(Gebruiker gebruiker)
        {
            _context.Gebruikers.Add(gebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGebruiker", new { id = gebruiker.GebruikerId }, gebruiker);
        }

        // DELETE: api/Gebruiker/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gebruiker>> DeleteGebruiker(int id)
        {
            var gebruiker = await _context.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            _context.Gebruikers.Remove(gebruiker);
            await _context.SaveChangesAsync();

            return gebruiker;
        }

        private bool GebruikerExists(int id)
        {
            return _context.Gebruikers.Any(e => e.GebruikerId == id);
        }
    }
}
