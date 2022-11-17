using System.Threading.Tasks;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<IActionResult> Register(RegisterDTO register)
		{
			if (register.Password != register.PasswordConfirm)
			{
				return StatusCode(StatusCodes.Status400BadRequest, 
					new { Message = "Passwords don't match." });
			}

			Assassin assassin = new Assassin()
			{
				UserName = register.UserName,
				Email = register.Email
			};

			IdentityResult result = await _UserManager.CreateAsync(assassin, register.Password);
			if (!result.Succeeded)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new { Message = "Could not register assassin." });
			}

			return Ok();
		}
	}
}
