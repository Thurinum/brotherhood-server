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

		[HttpPost]
		[Authorize]
		[Route("contract/target/create")]
		public async Task<ActionResult<IEnumerable<ContractTarget>>> CreateContractTarget()
		{
			IFormCollection form = await Request.ReadFormAsync();
			StringValues json = new();

			if (!form.TryGetValue("model", out json))
			{
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = $"New contract target data was not sent to the server. Please try again." });
			}

			ContractTarget target;

			try
			{
				target = JsonSerializer.Deserialize<ContractTarget>(json.ToString(), new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			} catch (JsonException)
			{
				return StatusCode(StatusCodes.Status422UnprocessableEntity, new { Message = $"Failed to process contract target data. Please try again." });
			}

			// add entity
			_context.ContractTargets.Add(target);
			await _context.SaveChangesAsync();

			target = await _context.ContractTargets.OrderBy(c => c.Id).LastAsync();

			// save image file
			IFormFile smImage = form.Files.GetFile("image-sm");
			IFormFile lgImage = form.Files.GetFile("image-lg");

			if (smImage == null || lgImage == null)
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "You must upload an image at least 1024x1024 pixels." });

			switch (ImageHelper.Upload(smImage, "targets", target.Id, ImageHelper.Size.sm))
			{
				case ImageHelper.Status.TooSmall:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 1024x1024 pixels." });
				case ImageHelper.Status.Invalid:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is invalid. Please upload a valid image." });
			}

			switch (ImageHelper.Upload(lgImage, "targets", target.Id, ImageHelper.Size.lg))
			{
				case ImageHelper.Status.TooSmall:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 1024x1024 pixels." });
				case ImageHelper.Status.Invalid:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is invalid. Please upload a valid image." });
			}

			target.ImageCacheId = Guid.NewGuid().ToString();

			// update entity with image
			_context.ContractTargets.Update(target);
			await _context.SaveChangesAsync();

			return CreatedAtAction("CreateContractTarget", new { id = target.Id }, target);
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

			// upload new image if applicable
			IFormFile smImage = form.Files.GetFile("image-sm");
			IFormFile lgImage = form.Files.GetFile("image-lg");

			if (smImage != null && lgImage != null)
			{
				switch (ImageHelper.Upload(smImage, "targets", updatedTarget.Id, ImageHelper.Size.sm))
				{
					case ImageHelper.Status.TooSmall:
						return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 1024x1024 pixels." });
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

				updatedTarget.ImageCacheId = Guid.NewGuid().ToString();
			}

			// save changes to model
			_context.ChangeTracker.Clear();
			_context.ContractTargets.Update(updatedTarget);

			try { await _context.SaveChangesAsync(); }
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Something went wrong when adding target {target.FirstName} {target.LastName} to this contract." });
			}

			return NoContent();
		}

		[HttpDelete]
		[Authorize]
		[Route("contract/target/{id}/delete/soft")]
		public async Task<ActionResult<ContractTarget>> SoftDeleteContractTarget(int id)
		{
			ContractTarget target = await _context.ContractTargets.FindAsync(id);

			if (target == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Requested contract target was not found. Please make sure a target is selected and try again." });

			// remove from all contracts
			target.Contracts.ForEach(c => c.Targets.Remove(target));
			_context.ContractTargets.Update(target);
			await _context.SaveChangesAsync();

			return NoContent();
		}
		
		[HttpDelete]
		[Authorize]
		[Route("contract/target/{id}/delete/hard")]
		public async Task<ActionResult<ContractTarget>> HardDeleteContractTarget(int id)
		{
			ContractTarget target = await _context.ContractTargets.FindAsync(id);

			if (target == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Requested contract target was not found. Please make sure a target is selected and try again."});
			
			// prevent removal if resource is in use
			if (target.Contracts.Any())
				return StatusCode(StatusCodes.Status409Conflict, new { Message = $"{target.FirstName} {target.LastName} is currently being targeted by one or more contracts. Please unassign him/her from all contracts or mark him/her as eliminated and and try again." });

			_context.ContractTargets.Remove(target);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private async Task<Assassin> GetCurrentUser() => await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
