using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Brotherhood_Server.Data;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Brotherhood_Server.Controllers
{
	[ApiController]
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

		// get all cities
		[HttpGet]
		[Route("cities")]
		public async Task<ActionResult<IEnumerable<City>>> GetCities()
		{
			return await _context.Cities.ToListAsync();
		}
	}
}
