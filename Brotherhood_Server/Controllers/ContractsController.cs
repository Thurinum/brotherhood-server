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
	[ApiController]
	[Route("api")]
	public class ContractsController : ControllerBase
	{
		private readonly BrotherhoodServerContext _context;
		private readonly UserManager<Assassin> _userManager;

		public ContractsController(BrotherhoodServerContext context, UserManager<Assassin> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("contracts")]
		[Route("contracts/public")]
		public async Task<ActionResult<IEnumerable<Contract>>> GetPublic()
		{
			return await _context.Contracts.Where(c => c.IsPublic).ToListAsync();
		}

		[HttpGet]
		[Authorize]
		[Route("contracts/private")]
		public async Task<ActionResult<IEnumerable<Contract>>> GetPrivate()
		{
			return (await GetCurrentUser()).Contracts;
		}

		[HttpGet]
		[Route("contract/{id}/targets")]
		public async Task<ActionResult<IEnumerable<ContractTarget>>> GetTargets(int id)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return NotFound();

			Assassin user = await GetCurrentUser();
			if (!contract.IsPublic && contract.Assassins.SingleOrDefault(a => a.Id == user.Id) == null)
				return StatusCode(StatusCodes.Status403Forbidden,
					new { Message = "You must be assigned to this contract in order to view its targets." });

			return contract.Targets;
		}

		[HttpPost]
		[Authorize]
		[Route("contract/add")]
		[Route("contract/create")]
		public async Task<ActionResult<Contract>> Create(Contract contract)
		{
			Assassin user = await GetCurrentUser();
			contract.Assassins = new List<Assassin> { user };

			_context.Contracts.Add(contract);
			await _context.SaveChangesAsync();

			return CreatedAtAction("CreateContract", new { id = contract.Id }, contract);
		}

		[HttpPut]
		[Authorize]
		[Route("contract/{id}/share")]
		public async Task<IActionResult> Share(int id, string shareeName)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"The contract provided with id '{id}' was not found." });

			Assassin user = await GetCurrentUser();
			if (contract.Assassins.SingleOrDefault(a => a.Id == user.Id) == null)
				return StatusCode(StatusCodes.Status403Forbidden,
					new { Message = "You must be assigned to this contract in order to request a partner." }
				);

			Assassin sharee = await _userManager.FindByNameAsync(shareeName);

			if (sharee == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Assassin {shareeName} does not exist." });

			if (contract.Assassins.SingleOrDefault(a => a.Id == sharee.Id) != null)
				return StatusCode(StatusCodes.Status302Found, new { Message = $"Assassin {sharee.FirstName} {sharee.LastName} is already assigned to this contract." });

			contract.Assassins.Add(sharee);

			_context.Entry(contract).State = EntityState.Modified;

			try { await _context.SaveChangesAsync(); }
			catch (DbUpdateConcurrencyException)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong when adding {sharee.FirstName} {sharee.LastName} to this contract." });
			}

			return NoContent();
		}

		[HttpPut]
		[Authorize]
		[Route("contract/{id}/target/add")]
		public async Task<IActionResult> AddTarget(int id, ContractTarget target)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Invalid contract id {id} provided." });

			Assassin user = await GetCurrentUser();

			if (!contract.Assassins.Contains(user))
				return StatusCode(StatusCodes.Status401Unauthorized, new { Message = $"You must be assigned to this contract in order to modify it." });

			contract.Targets.Add(target);
			_context.Entry(contract).State = EntityState.Modified;

			try { await _context.SaveChangesAsync(); }
			catch (Exception) { 
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when adding target {target.FirstName} {target.LastName} to this contract." });}

			return NoContent();
		}

		[HttpDelete]
		[Authorize]
		[Route("contract/{id}/nuke")]
		[Route("contract/{id}/remove")]
		public async Task<IActionResult> Delete(int id)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return NotFound($"The city {id} doesn't exist.");

			Assassin user = await GetCurrentUser();
			if (contract.Assassins.SingleOrDefault(a => a.Id == user.Id) == null)
				return StatusCode(
					StatusCodes.Status403Forbidden,
					new { Message = "You must be assigned to this contract in order to cancel it." }
				);

			_context.Contracts.Remove(contract);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private async Task<Assassin> GetCurrentUser() => await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
