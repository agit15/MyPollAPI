﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollAPI.Models;

namespace PollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntwoordController : ControllerBase
    {
        private readonly PollContext _context;

        public AntwoordController(PollContext context)
        {
            _context = context;
        }

        // GET: api/Antwoord
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoorden()
        {
            return await _context.Antwoorden.ToListAsync();
        }

        [Authorize]
        [HttpGet("poll/{pollId}")]
        public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoordById(int pollId)
        {
            return await _context.Antwoorden.Where(c => c.PollId == pollId).ToListAsync();
        }

        // GET: api/Antwoord/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Antwoord>> GetAntwoord(int id)
        {
            var antwoord = await _context.Antwoorden.FindAsync(id);

            if (antwoord == null)
            {
                return NotFound();
            }

            return antwoord;
        }

        // PUT: api/Antwoord/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAntwoord(int id, Antwoord antwoord)
        {
            if (id != antwoord.AntwoordId)
            {
                return BadRequest();
            }

            _context.Entry(antwoord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AntwoordExists(id))
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

        // POST: api/Antwoord
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Antwoord>> PostAntwoord(Antwoord antwoord)
        {
            _context.Antwoorden.Add(antwoord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAntwoord", new { id = antwoord.AntwoordId }, antwoord);
        }

        // DELETE: api/Antwoord/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Antwoord>> DeleteAntwoord(int id)
        {
            var antwoord = await _context.Antwoorden.FindAsync(id);
            if (antwoord == null)
            {
                return NotFound();
            }

            _context.Antwoorden.Remove(antwoord);
            await _context.SaveChangesAsync();

            return antwoord;
        }

        private bool AntwoordExists(int id)
        {
            return _context.Antwoorden.Any(e => e.AntwoordId == id);
        }
    }
}
