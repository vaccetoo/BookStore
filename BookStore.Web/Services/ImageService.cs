using BookStore.Web.Contracts;

namespace BookStore.Web.Services
{
	public class ImageService : IImageService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ImageService(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public string SaveImage(IFormFile file, string relativeFolderPath)
		{
			string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			string fullFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, relativeFolderPath);

			if (!Directory.Exists(fullFolderPath))
			{
				Directory.CreateDirectory(fullFolderPath);
			}

			string filePath = Path.Combine(fullFolderPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return Path.Combine(relativeFolderPath, fileName).Replace("\\", "/");
		}

		public void DeleteImage(string? relativeFilePath)
		{
			if (string.IsNullOrWhiteSpace(relativeFilePath)) return;

			string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativeFilePath);

			if (File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}
		}
	}
}
