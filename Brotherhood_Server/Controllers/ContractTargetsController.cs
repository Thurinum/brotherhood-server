using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Brotherhood_Server.Data;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Brotherhood_Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class ContractTargetsController : ControllerBase
    {
		private readonly BrotherhoodServerContext _context;
		private readonly UserManager<Assassin> _userManager;

		public ContractTargetsController(BrotherhoodServerContext context, UserManager<Assassin> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		[HttpPost]
		[Authorize]
		[Route("contract/target/create")]
		public async Task<ActionResult<ContractTarget>> CreateContractTarget(ContractTarget target)
		{
			_context.ContractTargets.Add(target);
			await _context.SaveChangesAsync();

			return CreatedAtAction("CreateContractTarget", new { id = target.Id }, target);
		}

		[HttpDelete]
		[Authorize]
		[Route("contract/target/{id}/nuke")]
		public async Task<ActionResult<ContractTarget>> DeleteContractTarget(int id)
		{
			ContractTarget target = await _context.ContractTargets.FindAsync(id);

			if (target == null)
				return NotFound($"The city {id} doesn't exist.");

			// TODO: Only admins may delete targets
			Assassin user = await GetCurrentUser();
			if (target.Contracts.Where(c => c.Assassins.Contains(user)).Count() == 0)
				return StatusCode(StatusCodes.Status403Forbidden, new { Message = "You must have a contract involving this target in order to cancel it." });

			_context.ContractTargets.Remove(target);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private async Task<Assassin> GetCurrentUser() => await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
