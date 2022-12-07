﻿using System;
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
using Microsoft.Net.Http.Headers;

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

		/// <summary>
		/// Authenticate a user with username/email and password.
		/// </summary>
		/// <remarks>This method is available without authentication.</remarks>
		/// <exception cref="StatusCodes.Status400BadRequest">If the provided credentials are invalid.</exception>
		/// <param name="login">A DTO containing the credentials needed for the login attempt.</param>
		/// <returns>
		///	An object containing a token to be used for authenticating to the API, as well as
		///	the token's expiration date and the current user's full name and autorization role.
		/// </returns>
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
				userName = $"{user.FirstName} {user.LastName}",
				role = userRoles.FirstOrDefault() // only one role per user
			});
		}

		/// <summary>
		/// Obtains the list of all publicly available users (that is, users without the Mentor special role).
		/// </summary>
		/// <exception cref="StatusCodes.Status400BadRequest">If the provided credentials are invalid.</exception>
		/// <returns>
		///	A list of objects containing the ids, user names, full names, emails, and roles of all non-admin users.
		/// </returns>
		[HttpGet]
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
				Email = register.Email,
				Role = "Assassin"
			};

			IdentityResult result = await _userManager.CreateAsync(user, register.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Could not register assassin." });

			await _userManager.AddToRoleAsync(user, "Assassin");

			return Ok();
		}

		[HttpDelete]
		[Authorize(Roles = "Mentor")]
		[Route("user/{id:Guid}/delete")]
		public async Task<ActionResult> DeleteUser(Guid id)
		{
			// refuse if user doesn't exist
			User user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Cannot delete user {id} because it does not exist." });

			// refuse if user attempts to delete itself
			User currentUser = await GetCurrentUser();
			if (user == currentUser)
				return StatusCode(StatusCodes.Status409Conflict, new { Message = $"The current user cannot delete itself." });

			// refuse if user is a mentor
			if (await _userManager.IsInRoleAsync(user, "Mentor"))
				return StatusCode(StatusCodes.Status405MethodNotAllowed, new { Message = $"Removal of mentor users is not supported." });

			await _userManager.DeleteAsync(user);

			return Ok();
		}

		private async Task<User> GetCurrentUser() => await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
