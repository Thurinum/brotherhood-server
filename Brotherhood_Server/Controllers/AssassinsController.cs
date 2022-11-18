﻿using System;
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
	[Route("api/[controller]")]
	[ApiController]
	public class AssassinsController : ControllerBase
	{
		private UserManager<Assassin> _UserManager;
		private IConfiguration _Configuration;

		public AssassinsController(UserManager<Assassin> userManager, IConfiguration configuration)
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

			Assassin assassin = new()
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
			Assassin assassin = await _UserManager.FindByNameAsync(login.UserName);

			if (assassin == null && !(await _UserManager.CheckPasswordAsync(assassin, login.Password)))
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Invalid username or password." });

			IList<string> roles = await _UserManager.GetRolesAsync(assassin);
			List<Claim> authClaims = new List<Claim>();

			foreach (string role in roles)
				authClaims.Add(new Claim(ClaimTypes.Role, role));

			authClaims.Add(new Claim(ClaimTypes.NameIdentifier, assassin.Id));

			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Secret"]));
			JwtSecurityToken token = new JwtSecurityToken(
				issuer: "https://localhost:4200",
				audience: "https://localhost:44386",
				claims: authClaims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
			);

			return Ok(new
			{
				token = new JwtSecurityTokenHandler().WriteToken(token),
				validTo = token.ValidTo
			});
		}
	}
}
