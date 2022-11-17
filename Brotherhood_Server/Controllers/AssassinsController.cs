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
using Microsoft.IdentityModel.Tokens;

namespace Brotherhood_Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssassinsController : ControllerBase
	{
		UserManager<Assassin> _UserManager;

		public AssassinsController(UserManager<Assassin> userManager)
		{
			_UserManager = userManager;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register(RegisterDTO register)
		{
			if (register.Password != register.PasswordConfirm)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Passwords don't match." });

			Assassin assassin = new Assassin()
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

			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Though yet of Hamlet our dear brother's death,The memory be green, and that it us befittedTo bear our hearts in grief, and our whole kingdomTo be contracted in one brow of woe;Yet so far has discretion fought with natureThat we with wisest sorrow think on himTogether with remembrance of ourselves.Therefore our sometimes sister, now our queen,Th' imperial jointress of this warlike state,Have we (as 'twere with a defeated joy,With one auspicious and one dropping eye,With mirth in funeral and with dirge in marriage,In equal scale weighing delight and dole) Taken to wife. Nor have we herein barred  Your better wisdoms which have freely goneWith this affair along. For all, our thanks.Now follows that you know — young Fortinbras, Holding a weak supposal of our worth,Or thinking by our late dear brother's deathOur state to be disjoint and out of frame,Colleagued with the dream of his advantage,He has not failed to pester us with messages,Importing the surrender of those landsLost by his father with all bonds of lawTo our most valiant brother. So much for him.[Enter messengers]Now for ourself and for this time of meeting. Thus much the business is: we have here writTo Norway (uncle of young FortinbrasWho, impotent and bed-rid, scarcely hearsOf this his nephew's purpose) to suppressHis further gait herein in that the levies,The lists, and full proportions are all madeOut of his subjects. And we here dispatch You, good Cornelius, and you, Voltemand,For bearing of this greeting to old Norway,Giving to you no further personal powerTo business with the king more than the scopeOf these delated articles allow.Farewell, and let your haste commend your duty."));
			JwtSecurityToken token = new JwtSecurityToken(
				issuer: "https://localhost:4200",
				audience: "https://localhost:44386",
				claims: authClaims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
			);

			return Ok();
		}
	}
}
