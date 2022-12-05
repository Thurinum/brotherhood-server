using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Brotherhood_Server
{
	public class ImageHelper
	{
		public enum Size
		{
			sm = 256,
			lg = 1024
		}

		public enum Status
		{
			TooSmall,
			Invalid,
			Success
		}

		public static Status Upload(IFormFile file, string subFolder, int entityId, Size size)
		{
			Image image;

			try   { image = Image.Load(file.OpenReadStream()); }
			catch { return Status.Invalid; }

			// refuse if image is too small or too large
			int width = (int)size;
			if (image.Width < width * 0.5 || image.Width > width)
				return Status.TooSmall;

			string path = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{subFolder}/{Enum.GetName(size)}";

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				Console.WriteLine($"Created directory '{path} for storing images.");
			}

			image.SaveAsWebp($"{path}/{entityId}.webp");

			return Status.Success;
		}

		public static bool Delete(string subFolder, int entityId, Size size)
		{
			string path = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{subFolder}/{Enum.GetName(size)}/{entityId}.webp";

			try { File.Delete(path); }
			catch { return false; }

			return true;
		}
	}
}
