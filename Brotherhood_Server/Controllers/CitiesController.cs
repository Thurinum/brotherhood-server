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
using Microsoft.AspNetCore.Authorization;

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
		// GET: api/cities/public
		[HttpGet]
		[Route("")]
		[Route("public")]
		public async Task<ActionResult<IEnumerable<City>>> GetCities()
		{
			return await _context.City.Where(c => c.IsPublic).ToListAsync();
		}

		// GET: api/cities/user
		[Authorize]
		[HttpGet]
		[Route("user")]
		public async Task<ActionResult<IEnumerable<City>>> GetUserCities()
		{
			return (await GetCurrentUser()).Cities;
		}

		// GET: api/cities/69
		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<City>> GetCity(int id)
		{
			return StatusCode(
				StatusCodes.Status501NotImplemented,
				new { Message = "This feature is unimplemented. See you in TP4." }
			);

			/*var city = await _context.City.FindAsync(id);

			if (city == null)
				return NotFound();

			return city;*/
		}

		// POST: api/cities/add
		[Authorize]
		[HttpPost]
		[Route("add")]
		public async Task<ActionResult<City>> PostCity(City city)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Assassin assassin = await _UserManager.FindByIdAsync(userId);
			city.Assassins = new List<Assassin> { assassin };
			_context.City.Add(city);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCity", new { id = city.Id }, city);
		}

		// PUT: api/cities/69/edit
		[Authorize]
		[HttpPut]
		[Route("share/{assassinId}")]
		public async Task<IActionResult> AddCityAssassin(string assassinId, City cityDTO)
		{
			if (cityDTO == null)
				return BadRequest("The provided city is invalid.");

			City city = _context.City.SingleOrDefault(c => c.Id == cityDTO.Id);

			if (city == null)
				return BadRequest("The provided city is invalid.");

			Assassin user = await GetCurrentUser();
			if (city.Assassins.SingleOrDefault(a => a.Id == user.Id) == null)
				return StatusCode(
					StatusCodes.Status403Forbidden,
					new { Message = "Ownership of this object is required to modify it." }
				);

			Assassin sharee = await _UserManager.FindByNameAsync(assassinId) ?? await _UserManager.FindByEmailAsync(assassinId);

			if (sharee == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"User {assassinId} does not exist." });

			city.Assassins.Add(sharee);

			_context.Entry(city).State = EntityState.Modified;

			try { await _context.SaveChangesAsync(); }
			catch (DbUpdateConcurrencyException)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong when adding owner." });
			}

			return NoContent();
		}

		// DELETE: api/cities/69/nuke
		[Authorize]
		[HttpDelete]
		[Route("{id}/nuke")]
		public async Task<IActionResult> DeleteCity(int id)
		{
			var city = await _context.City.FindAsync(id);
			if (city == null)
				return NotFound();

			_context.City.Remove(city);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private async Task<Assassin> GetCurrentUser() => await _UserManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
