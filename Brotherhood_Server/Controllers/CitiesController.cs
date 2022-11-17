using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Brotherhood_Server.Data;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Brotherhood_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly BrotherhoodServerContext _context;
		private UserManager<Assassin> _UserManager;

        public CitiesController(BrotherhoodServerContext context, UserManager<Assassin> userManager)
        {
            _context = context;
			_UserManager = userManager;
        }

		// GET: api/cities
        [HttpGet]
		public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            return await _context.City.ToListAsync();
        }

        // GET: api/cities/69
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.City.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/cities/69
        [HttpPut]
		[Route("{id}/edit")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/cities/69
        [HttpPost]
		[Route("add")]
		public async Task<ActionResult<City>> PostCity(City city)
        {
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Assassin assassin = await _UserManager.FindByIdAsync(userId);
			city.Assassins = new List<Assassin>();
			city.Assassins.Add(assassin);
			_context.City.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete]
		[Route("{id}/nuke")]
		public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}
