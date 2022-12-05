using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.IO;

namespace Brotherhood_Server.Services
{
	public class ImageService : IImageService
	{
		public ImageUploadStatus Upload(IFormFile file, string subFolder, int entityId, ImageSize size)
		{
			Image image;

			try { image = Image.Load(file.OpenReadStream()); }
			catch { return ImageUploadStatus.Invalid; }

			// refuse if image is too small or too large
			int width = (int)size;
			if (image.Width < width * 0.5 || image.Width > width)
				return ImageUploadStatus.TooSmall;

			string path = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{subFolder}/{Enum.GetName(size)}";

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				Console.WriteLine($"Created directory '{path} for storing images.");
			}

			image.SaveAsWebp($"{path}/{entityId}.webp");

			return ImageUploadStatus.Success;
		}

		public bool Delete(string subFolder, int entityId, ImageSize size)
		{
			string path = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{subFolder}/{Enum.GetName(size)}/{entityId}.webp";

			try { File.Delete(path); }
			catch { return false; }

			return true;
		}
	}
}
