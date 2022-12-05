using Brotherhood_Server.Data;
using Brotherhood_Server.Models;
using Brotherhood_Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Brotherhood_Server.Controllers
{
	[Route("api")]
	[Authorize]
	[ApiController]
	public class ContractTargetsController : ControllerBase
	{
		private readonly BrotherhoodServerContext _context;
		private readonly UserManager<User> _userManager;
		private readonly ImageService _imageService;

		public ContractTargetsController(BrotherhoodServerContext context, UserManager<User> userManager, ImageService imageService)
		{
			_context = context;
			_userManager = userManager;
			_imageService = imageService;
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

			// check for image file
			IFormFile smImage = form.Files.GetFile("image-sm");
			IFormFile lgImage = form.Files.GetFile("image-lg");

			if (smImage == null || lgImage == null)
			{
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "You must upload an image at least 1024x1024 pixels." });
			}

			// parse model json
			ContractTarget target;
			try
			{
				target = JsonSerializer.Deserialize<ContractTarget>(json.ToString(), new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			}
			catch (JsonException)
			{
				return StatusCode(StatusCodes.Status422UnprocessableEntity, new { Message = $"Failed to process contract target data. Please try again." });
			}

			// refuse if model invalid
			ValidationContext context = new(target, null, null);
			List<ValidationResult> validationResults = new();

			if (!Validator.TryValidateObject(target, context, validationResults, true))
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Some fields aren't filled. You know what they are. Go fill em'." });

			// add entity
			_context.ContractTargets.Add(target);
			await _context.SaveChangesAsync();

			target = await _context.ContractTargets.OrderBy(c => c.Id).LastAsync();

			// save image file
			switch (_imageService.Upload(smImage, "targets", target.Id, ImageSize.sm))
			{
				case ImageUploadStatus.TooSmall:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 1024x1024 pixels." });
				case ImageUploadStatus.Invalid:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is invalid. Please upload a valid image." });
			}

			switch (_imageService.Upload(lgImage, "targets", target.Id, ImageSize.lg))
			{
				case ImageUploadStatus.TooSmall:
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 1024x1024 pixels." });
				case ImageUploadStatus.Invalid:
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

			// refuse if user has no contract with that target
			User user = await GetCurrentUser();
			IList<User> mentors = await _userManager.GetUsersInRoleAsync("Mentor");

			if (user.Contracts.Any(c => c.Targets.Contains(target)) && !mentors.Contains(user))
				return StatusCode(StatusCodes.Status401Unauthorized, new { Message = $"You must have at least one contract targeting {target.FirstName} {target.LastName} in order to modify him/her." });

			// deserialize model from json
			IFormCollection form = await Request.ReadFormAsync();
			StringValues json = new();

			form.TryGetValue("model", out json);

			ContractTarget updatedTarget = JsonSerializer.Deserialize<ContractTarget>(json.ToString(), new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			// refuse if model invalid
			ValidationContext context = new(updatedTarget, null, null);
			List<ValidationResult> validationResults = new();

			if (!Validator.TryValidateObject(updatedTarget, context, validationResults, true))
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Some fields aren't filled. You know what they are. Go fill em'." });

			// upload new image if applicable
			IFormFile smImage = form.Files.GetFile("image-sm");
			IFormFile lgImage = form.Files.GetFile("image-lg");

			if (smImage != null && lgImage != null)
			{
				switch (_imageService.Upload(smImage, "targets", updatedTarget.Id, ImageSize.sm))
				{
					case ImageUploadStatus.TooSmall:
						return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 1024x1024 pixels." });
					case ImageUploadStatus.Invalid:
						return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is invalid. Please upload a valid image." });
				}

				switch (_imageService.Upload(lgImage, "targets", updatedTarget.Id, ImageSize.lg))
				{
					case ImageUploadStatus.TooSmall:
						return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The image you uploaded is too small. Please upload an image that is at least 1024x1024 pixels." });
					case ImageUploadStatus.Invalid:
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
		[Authorize(Roles = "Mentor")]
		[Route("contract/target/{id}/delete/soft")]
		public async Task<ActionResult<ContractTarget>> SoftDeleteContractTarget(int id)
		{
			ContractTarget target = await _context.ContractTargets.FindAsync(id);

			if (target == null)
			{
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Requested contract target was not found. Please make sure a target is selected and try again." });
			}

			// remove from all contracts
			target.Contracts.ForEach(c =>
			{
				if (c.CoverTargetId == target.Id)
				{
					c.CoverTargetId = 0;
				}

				c.Targets.Remove(target);
			});
			_context.ContractTargets.Update(target);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete]
		[Authorize(Roles = "Mentor")]
		[Route("contract/target/{id}/delete/hard")]
		public async Task<ActionResult<ContractTarget>> HardDeleteContractTarget(int id)
		{
			ContractTarget target = await _context.ContractTargets.FindAsync(id);

			if (target == null)
				return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Requested contract target was not found. Please make sure a target is selected and try again." });

			target.Contracts.ForEach(c =>
			{
				if (c.CoverTargetId == target.Id)
					c.CoverTargetId = 0;
			});
			_context.ContractTargets.Remove(target);
			await _context.SaveChangesAsync();

			_imageService.Delete("targets", id, ImageSize.sm);
			_imageService.Delete("targets", id, ImageSize.lg);

			return NoContent();
		}

		private async Task<User> GetCurrentUser() => await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
