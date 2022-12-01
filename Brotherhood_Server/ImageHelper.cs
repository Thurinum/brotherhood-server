using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Brotherhood_Server
{
	public class ImageHelper
	{
		public static void Upload(IFormFile file, string subFolder, int entityId)
		{
			Image image = Image.Load(file.OpenReadStream());
			string path = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{subFolder}";

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				Console.WriteLine($"Created directory '{path} for storing images.");
			}

			image.SaveAsWebp($"{path}/{entityId}.webp");
		} 
	}
}
