using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.Threading.Tasks;

namespace Brotherhood_Server
{
	public class ImageHelper
	{
		public static string Upload(IFormFile file, string subFolder)
		{
			string imageId = Guid.NewGuid().ToString();
			Image image = Image.Load(file.OpenReadStream());

			image.SaveAsWebp($"/wwwroot/images/{subFolder}/{imageId}.webp");

			return imageId;
		}
	}
}
