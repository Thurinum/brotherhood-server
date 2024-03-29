﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Brotherhood_Server.Data;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brotherhood_Server.Controllers
{
	/// <summary>
	///	Handles cities. Cities cannot be managed in this version of the API.
	/// </summary>
	[ApiController]
	[Authorize]
	[Route("api")]
	public class CitiesController : ControllerBase
	{
		private readonly BrotherhoodServerContext _context;
		private readonly UserManager<User> _userManager;

		public CitiesController(BrotherhoodServerContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		/// <summary>
		///	Gets a list of all cities.
		/// </summary>
		/// <remarks>This method does not require authentication.</remarks>
		/// <returns>A list of City objects.</returns>
		[HttpGet]
		[AllowAnonymous]
		[Route("cities")]
		public async Task<ActionResult<IEnumerable<City>>> GetCities()
		{
			return await _context.Cities.ToListAsync();
		}
	}
}
