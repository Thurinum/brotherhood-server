using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.IO;

namespace Brotherhood_Server.Services
{
	public enum ImageSize
	{
		sm = 256,
		lg = 1024
	}

	public enum ImageUploadStatus
	{
		TooSmall,
		Invalid,
		Success
	}

	public interface IImageService
	{
		public bool Delete(string subFolder, int entityId, ImageSize size);
		public ImageUploadStatus Upload(IFormFile file, string subFolder, int entityId, ImageSize size);
	}
}