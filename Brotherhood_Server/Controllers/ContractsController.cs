using Brotherhood_Server.Data;
using Brotherhood_Server.Models;
using Brotherhood_Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Brotherhood_Server.Controllers
{
	/// <summary>
	///	Handles the management of contracts.
	///	Contracts are assignments that involve zero or more Contract Targets.
	///	Contracts may be assigned targets, may be edited, deleted, etc.
	///	One of a contract's targets may be chosen as its cover.
	/// </summary>
	[ApiController]
	[Authorize]
	[Route("api")]
	public class ContractsController : ControllerBase
	{
		private readonly BrotherhoodServerContext _context;
		private readonly UserManager<User> _userManager;
		private readonly IErrorService _error;

		public ContractsController(BrotherhoodServerContext context, UserManager<User> userManager, IErrorService errorService)
		{
			_context = context;
			_userManager = userManager;
			_error = errorService;
		}

		/// <summary>
		///	Obtains the list of all public contracts, that is, contracts accessible to all assassins.
		/// </summary>
		/// <remarks>This method does not require authentication.</remarks>
		/// <returns>
		///	A list of contracts.
		/// </returns>
		[HttpGet]
		[AllowAnonymous]
		[Route("contracts")]
		[Route("contracts/public")]
		public async Task<ActionResult<IEnumerable<Contract>>> GetPublicContracts()
		{
			return await _context.Contracts.Where(c => c.IsPublic).ToListAsync();
		}

		/// <summary>
		///	Obtains the list of contracts for the authenticated user.
		///	Those are contracts involving the current user.
		/// </summary>
		/// <returns>A list of contracts.</returns>
		[HttpGet]
		[Route("contracts/private")]
		public async Task<ActionResult<IEnumerable<Contract>>> GetPrivateContracts()
		{
			return (await GetCurrentUser()).Contracts;
		}

		/// <summary>
		///	Gets a summary of statistics on the database.
		/// </summary>
		/// <remarks>This method does not require authentication.</remarks>
		/// <returns>
		///	An object containing statistics on the number of contracts, public contracts,
		///	targets, users, and cities.
		/// </returns>
		[HttpGet]
		[AllowAnonymous]
		[Route("statistics")]
		public async Task<ActionResult<object>> GetStatistics()
		{
			return Ok(new
			{
				NbContracts = _context.Contracts.Count(),
				NbPublicContracts = _context.Contracts.Where(c => c.IsPublic).Count(),
				NbTargets = _context.ContractTargets.Count(),
				NbAssassins = _userManager.Users.Count(),
				NbCities = _context.Cities.Count()
			});
		}

		/// <summary>
		///	Creates a contract.
		/// </summary>
		/// <param name="contract">A DTO for the contract.</param>
		[HttpPost]
		[Route("contract/create")]
		public async Task<ActionResult<Contract>> CreateContract(Contract contract)
		{
			User user = await GetCurrentUser();
			contract.Assassins = new List<User> { user };

			_context.Contracts.Add(contract);
			await _context.SaveChangesAsync();

			return CreatedAtAction("CreateContract", new { id = contract.Id }, contract);
		}

		/// <summary>
		///	Obtains a list of all contract targets for a given contract.
		/// </summary>
		/// <param name="id">The id of the contract.</param>
		/// <exception cref="Status403Forbidden">If the user is not himself assigned to the contract.</exception>
		/// <returns>A list of targets for the given contract.</returns>
		[HttpGet]
		[Route("contract/{id}/targets")]
		public async Task<ActionResult<IEnumerable<ContractTarget>>> GetContractTargets(int id)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return NotFound();

			User user = await GetCurrentUser();
			var mentors = await _userManager.GetUsersInRoleAsync("Mentor");
			if (!contract.IsPublic && contract.Assassins.SingleOrDefault(a => a.Id == user.Id) == null && !mentors.Contains(user))
				return StatusCode(StatusCodes.Status403Forbidden,
					new { Message = "You must be assigned to this contract in order to view its targets." });

			return contract.Targets;
		}

		/// <summary>
		///	Assigns an existing target to a contract.
		/// </summary>
		/// <param name="id">The id of the contract.</param>
		/// <param name="target">A DTO of the target to be assigned to the contract.</param>
		/// <exception cref="Status400BadRequest">If the contract id provided is not valid.</exception>
		/// <exception cref="Status403Forbidden">If the user is not assigned to the contract.</exception>
		/// <exception cref="Status409Conflict">If the target is already assigned to the contract.</exception>
		/// <exception cref="Status500InternalServerError">If the assignment of the target to the contract fails.</exception>
		/// <returns></returns>
		[HttpPut]
		[Route("contract/{id}/target/add")]
		public async Task<IActionResult> AddContractTarget(int id, ContractTarget target)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Invalid contract id {id} provided." });

			// refuse is user isn't assigned to contract
			User user = await GetCurrentUser();
			var mentors = await _userManager.GetUsersInRoleAsync("Mentor");

			if (!contract.Assassins.Contains(user) && !mentors.Contains(user))
				return StatusCode(StatusCodes.Status403Forbidden, new { Message = $"You must be assigned to this contract in order to modify it." });

			// refuse if contract already contains target
			if (contract.Targets.Any(c => c.Id == target.Id))
				return StatusCode(StatusCodes.Status409Conflict, new { Message = $"Target {target.FirstName} {target.LastName} is already assigned to this contract." });

			contract.Targets.Add(target);
			_context.Entry(contract).State = EntityState.Modified;

			try { await _context.SaveChangesAsync(); }
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when adding target {target.FirstName} {target.LastName} to this contract." });
			}

			return NoContent();
		}

		/// <summary>
		///	Unassigns a target from a contract.
		/// </summary>
		/// <param name="id">The id of the contract.</param>
		/// <param name="dto">A dto of the contract target to remove.</param>
		/// <remarks>
		///	This does not delete the target: it remains assigned to other contracts, and remains
		///	available for use in other contracts.
		/// </remarks>
		/// <exception cref="Status400BadRequest">If the provided data is invalid.</exception>
		/// <exception cref="Status403Forbidden">If the user is not assigned to the contract.</exception>
		/// <exception cref="Status404NotFound">If the target is not assigned to the contract.</exception>
		/// <exception cref="Status500InternalServerError">If the server encounters an error unassigning the target.</exception>
		/// <returns></returns>
		[HttpPut]
		[Route("contract/{id}/target/remove")]
		public async Task<IActionResult> RemoveContractTarget(int id, ContractTarget dto)
		{
			Contract contract = await _context.Contracts.FindAsync(id);
			ContractTarget target = await _context.ContractTargets.FindAsync(dto.Id);

			if (contract == null)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Contract {id} does not exist." });
			if (target == null)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Contract target {dto.Id} does not exist." });

			User user = await GetCurrentUser();
			var mentors = await _userManager.GetUsersInRoleAsync("Mentor");
			if (!contract.Assassins.Contains(user) && !mentors.Contains(user))
				return StatusCode(StatusCodes.Status403Forbidden, new { Message = "You must be assigned to this contract in order to edit its targets." });
			if (!contract.Targets.Contains(target))
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Cannot remove target '{target.FirstName} {target.LastName}' from contract '{contract.Codename}' because it is not a target in the latter." });

			contract.Targets.Remove(target);
			_context.Entry(contract).State = EntityState.Modified;

			try { await _context.SaveChangesAsync(); }
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when removing target {target.FirstName} {target.LastName} from contract '{contract.Codename}'." });
			}

			return NoContent();
		}

		/// <summary>
		///	
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("contract/{id}/edit/cover")]
		public async Task<IActionResult> SetContractCover(int id, ContractTarget dto)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Invalid contract id {id} provided." });

			User user = await GetCurrentUser();

			var mentors = await _userManager.GetUsersInRoleAsync("Mentor");
			if (!contract.Assassins.Contains(user) && !mentors.Contains(user))
				return StatusCode(StatusCodes.Status403Forbidden, new { Message = $"You must be assigned to this contract in order to modify it." });

			ContractTarget target = await _context.ContractTargets.FindAsync(dto.Id);

			if (target == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Invalid contract target id {dto.Id} provided." });

			contract.CoverTargetId = target.Id;
			_context.Entry(contract).State = EntityState.Modified;

			target.ImageCacheId = Guid.NewGuid().ToString(); // not working?
			_context.ContractTargets.Update(target);

			try { await _context.SaveChangesAsync(); }
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when adding setting {target.FirstName} {target.LastName} as cover for this contract." });
			}

			return AcceptedAtAction("SetContractCover", new { id = contract.Id }, contract);
		}

		[HttpPut]
		[Route("contract/{id}/edit")]
		public async Task<IActionResult> UpdateContract(int id, Contract dto)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Invalid contract id {id} provided." });

			User user = await GetCurrentUser();

			var mentors = await _userManager.GetUsersInRoleAsync("Mentor");
			if (!contract.Assassins.Contains(user) && !mentors.Contains(user))
				return StatusCode(StatusCodes.Status403Forbidden, new { Message = $"You must be assigned to this contract in order to modify it." });

			_context.ChangeTracker.Clear();
			_context.Contracts.Update(dto);
			_context.Entry(dto).State = EntityState.Modified;

			try { await _context.SaveChangesAsync(); }
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when updating contract {contract.Codename}." });
			}

			return NoContent();
		}

		[HttpPut]
		[Route("contract/{id}/share")]
		public async Task<IActionResult> ShareContract(int id, User shareeDTO)
		{
			if (shareeDTO == null)
				return _error.Response(Status400BadRequest, "No sharee name was provided to the server. Please try again.");

			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"The contract provided with id '{id}' was not found." });

			User user = await GetCurrentUser();
			var mentors = await _userManager.GetUsersInRoleAsync("Mentor");
			if (contract.Assassins.SingleOrDefault(a => a.Id == user.Id) == null && !mentors.Contains(user))
				return StatusCode(StatusCodes.Status403Forbidden,
					new { Message = "You must be assigned to this contract in order to request a partner." }
				);

			User sharee = await _userManager.FindByIdAsync(shareeDTO.Id);

			if (sharee == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Assassin {shareeDTO} does not exist." });

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

		[HttpDelete]
		[Route("contract/{id}/nuke")]
		public async Task<IActionResult> DeleteContract(int id)
		{
			Contract contract = await _context.Contracts.FindAsync(id);

			if (contract == null)
				return StatusCode(Status400BadRequest, new { Message = $"City {id} does not exist." }); ;

			User user = await GetCurrentUser();
			var mentors = await _userManager.GetUsersInRoleAsync("Mentor");
			if (contract.Assassins.SingleOrDefault(a => a.Id == user.Id) == null && !mentors.Contains(user))
				return StatusCode(Status403Forbidden, new { Message = "You must be assigned to this contract in order to cancel it." });

			_context.Contracts.Remove(contract);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private async Task<User> GetCurrentUser() => await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
