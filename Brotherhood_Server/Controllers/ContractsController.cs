using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Brotherhood_Server.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Brotherhood_Server.Models;

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
			public async Task<ActionResult<IEnumerable<Contract>>> GetPublicContracts()
			{
				  return await _context.Contracts.Where(c => c.IsPublic).ToListAsync();
			}

			[HttpGet]
			[Authorize]
			[Route("contracts/private")]
			public async Task<ActionResult<IEnumerable<Contract>>> GetPrivateContracts()
			{
				  return (await GetCurrentUser()).Contracts;
			}

			[HttpGet]
			[Route("contract/{id}/targets")]
			public async Task<ActionResult<IEnumerable<ContractTarget>>> GetContractTargets(int id)
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

			[HttpPut]
			[Authorize]
			[Route("contract/{id}/target/add")]
			public async Task<IActionResult> AddContractTarget(int id, ContractTarget target)
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
				  catch (Exception)
				  {
						return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when adding target {target.FirstName} {target.LastName} to this contract." });
				  }

				  return NoContent();
			}

			[HttpPut]
			[Authorize]
			[Route("contract/{id}/target/{targetId}/remove")]
			public async Task<IActionResult> RemoveContractTarget(int id, int targetId)
			{
				  Contract contract = await _context.Contracts.FindAsync(id);
				  ContractTarget target = await _context.ContractTargets.FindAsync(targetId);

				  if (contract == null)
						return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Contract {id} does not exist." });
				  if (target == null)
						return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"Contract target {targetId} does not exist." });

				  Assassin user = await GetCurrentUser();
				  if (!contract.Assassins.Contains(user))
						return StatusCode(StatusCodes.Status401Unauthorized, new { Message = "You must be assigned to this contract in order to edit its targets." });
				  if (!contract.Targets.Contains(target))
						return StatusCode(StatusCodes.Status401Unauthorized, new { Message = $"Cannot remove target '{target.FirstName} {target.LastName}' from contract '{contract.Codename}' because it is not a target in the latter." });

				  contract.Targets.Remove(target);
				  _context.Entry(contract).State = EntityState.Modified;

				  try { await _context.SaveChangesAsync(); }
				  catch (Exception)
				  {
						return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when removing target {target.FirstName} {target.LastName} from contract '{contract.Codename}'." });
				  }

				  return NoContent();
			}

			[HttpPost]
			[Authorize]
			[Route("contract/create")]
			public async Task<ActionResult<Contract>> CreateContract(Contract contract)
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
			public async Task<IActionResult> ShareContract(int id, string shareeName)
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

			[HttpDelete]
			[Authorize]
			[Route("contract/{id}/nuke")]
			public async Task<IActionResult> DeleteContract(int id)
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
