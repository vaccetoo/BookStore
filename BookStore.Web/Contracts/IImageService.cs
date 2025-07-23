namespace BookStore.Web.Contracts
{
	public interface IImageService
	{
		string SaveImage(IFormFile file, string folder);
		void DeleteImage(string relativePath);
	}
}
