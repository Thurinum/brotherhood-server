using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Brotherhood_Server.Controllers
{
	[ApiController]
	[Route("api")]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<User> _UserManager;
		private readonly IConfiguration _Configuration;

		public UsersController(UserManager<User> userManager, IConfiguration configuration)
		{
			_UserManager = userManager;
			_Configuration = configuration;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register(RegisterDTO register)
		{
			if (register.Password != register.PasswordConfirm)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Passwords don't match." });

			User assassin = new()
			{
				UserName = register.UserName,
				Email = register.Email
			};

			IdentityResult result = await _UserManager.CreateAsync(assassin, register.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Could not register assassin." });

			return Ok();
		}

		[HttpPost]
		[Route("login")]
		public async Task<ActionResult> Login(LoginDTO login)
		{
			User assassin = login.Email == null 
				? await _UserManager.FindByNameAsync(login.UserName) 
				: await _UserManager.FindByEmailAsync(login.Email);

			if (assassin == null || !(await _UserManager.CheckPasswordAsync(assassin, login.Password)))
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Invalid identifier or password." });

			IList<string> roles = await _UserManager.GetRolesAsync(assassin);
			List<Claim> authClaims = new();

			foreach (string role in roles)
				authClaims.Add(new Claim(ClaimTypes.Role, role));

			authClaims.Add(new Claim(ClaimTypes.NameIdentifier, assassin.Id));

			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_Configuration["JWT:Secret"]));
			JwtSecurityToken token = new(
				audience: "http://localhost:4200",
				issuer: "https://localhost:5001",
				claims: authClaims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
			);

			return Ok(new
			{
				token = new JwtSecurityTokenHandler().WriteToken(token),
				validTo = token.ValidTo,
				username = $"{assassin.FirstName} {assassin.LastName}"
			});
		}
	}
}
