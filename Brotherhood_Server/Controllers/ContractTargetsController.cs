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
using SixLabors.ImageSharp;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Primitives;

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

		[HttpGet]
		[Route("contract/targets")]
		public async Task<ActionResult<IEnumerable<ContractTarget>>> GetContractTargets()
		{
			return await _context.ContractTargets.ToListAsync();
		}

		[HttpPut]
		[Authorize]
		[Route("contract/target/{id}/edit")]
		public async Task<ActionResult<IEnumerable<ContractTarget>>> UpdateContractTarget(int id)
		{
			ContractTarget target = await _context.ContractTargets.FindAsync(id);

			if (target == null)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"The entity to update with id {id} does not exist." });

			// deserialize model from json
			IFormCollection form = await Request.ReadFormAsync();
			StringValues json = new();

			form.TryGetValue("model", out json);

			ContractTarget updatedTarget = JsonSerializer.Deserialize<ContractTarget>(json.ToString(), new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			// save changes to model
			_context.ChangeTracker.Clear();
			_context.ContractTargets.Update(updatedTarget);

			try { await _context.SaveChangesAsync(); }
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when adding target {target.FirstName} {target.LastName} to this contract." });
			}

			// upload new image if applicable
			IFormFile smImage = form.Files.GetFile("image-sm");
			IFormFile lgImage = form.Files.GetFile("image-lg");

			if (smImage == null || lgImage == null)
				return NoContent();

			switch (ImageHelper.Upload(smImage, "targets", updatedTarget.Id, ImageHelper.Size.sm))
			{
				case ImageHelper.Status.TooSmall:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 128x128 pixels." });
				case ImageHelper.Status.Invalid:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is invalid. Please upload a valid image." });
			}

			switch (ImageHelper.Upload(lgImage, "targets", updatedTarget.Id, ImageHelper.Size.lg))
			{
				case ImageHelper.Status.TooSmall:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 1024x1024 pixels." });
				case ImageHelper.Status.Invalid:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is invalid. Please upload a valid image." });
			}

			return NoContent();
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
				return NotFound($"Contract target {id} does not exist.");

			// TODO: Only admins may delete targets
			Assassin user = await GetCurrentUser();
			if (!target.Contracts.Where(c => c.Assassins.Contains(user)).Any())
				return StatusCode(StatusCodes.Status403Forbidden, new { Message = "You must have a contract involving this target in order to cancel it." });

			_context.ContractTargets.Remove(target);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private async Task<Assassin> GetCurrentUser() => await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
