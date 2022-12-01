using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Brotherhood_Server
{
	public class ImageHelper
	{
		public static string Upload(IFormFile file, string subFolder)
		{
			string imageId = Guid.NewGuid().ToString();
			Image image = Image.Load(file.OpenReadStream());
			string path = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{subFolder}";

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			image.SaveAsWebp($"{path}/{imageId}.webp");

			return imageId;
		} 
	}
}
