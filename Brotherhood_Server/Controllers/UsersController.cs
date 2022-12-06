using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Brotherhood_Server.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api")]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _Configuration;

		public UsersController(UserManager<User> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_Configuration = configuration;
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("login")]
		public async Task<ActionResult> Login(LoginDTO login)
		{
			User user = login.Email == null 
				? await _userManager.FindByNameAsync(login.UserName) 
				: await _userManager.FindByEmailAsync(login.Email);

			if (user == null || !(await _userManager.CheckPasswordAsync(user, login.Password)))
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Invalid identifier or password." });

			IList<string> userRoles = await _userManager.GetRolesAsync(user);
			List<Claim> authClaims = new();

			foreach (string role in userRoles)
				authClaims.Add(new Claim(ClaimTypes.Role, role));

			authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

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
				username = $"{user.FirstName} {user.LastName}",
				role = userRoles.FirstOrDefault() // only one role per user
			});
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("users/public")]
		public async Task<IActionResult> GetPublicUsers()
		{
			IList<User> assassins = await _userManager.GetUsersInRoleAsync("Assassin");
			return Ok(assassins.Select(a => new User
			{
				Id = a.Id,
				UserName = a.UserName,
				FirstName = a.FirstName,
				LastName = a.LastName,
				Email = a.Email,
				Role = "Assassin"
			}).ToList());
		}

		[HttpGet]
		[Authorize(Roles = "Mentor")]
		[Route("users/private")]
		public async Task<IActionResult> GetAllUsers()
		{
			IList<User> mentors = await _userManager.GetUsersInRoleAsync("Mentor");
			return Ok(_userManager.Users.Select(a => new User
			{
				Id = a.Id,
				UserName = a.UserName,
				FirstName = a.FirstName,
				LastName = a.LastName,
				Email = a.Email,
				Role = mentors.Contains(a) ? "Mentor" : "Assassin"
			}).ToList());
		}

		[HttpPost]
		[Authorize(Roles = "Mentor")]
		[Route("user/create")]
		public async Task<IActionResult> CreateUser(RegisterDTO register)
		{
			if (register.Password != register.PasswordConfirm)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Passwords don't match." });

			User user = new()
			{
				UserName = register.UserName,
				FirstName = register.FirstName,
				LastName = register.LastName,
				Email = register.Email
			};

			IdentityResult result = await _userManager.CreateAsync(user, register.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Could not register assassin." });

			await _userManager.AddToRoleAsync(user, "assassin");

			return Ok();
		}

		[HttpDelete]
		[Authorize(Roles = "Mentor")]
		[Route("user/{id:Guid}/delete")]
		public async Task<ActionResult> DeleteUser(Guid id)
		{
			User user = await _userManager.FindByIdAsync(id.ToString());

			if (user == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Cannot delete user {id} because it does not exist." });

			await _userManager.DeleteAsync(user);

			return Ok();
		}
	}
}
