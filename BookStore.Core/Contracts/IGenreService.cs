using BookStore.Core.Models.Genre;

namespace BookStore.Core.Contracts
{
	public interface IGenreService
	{
		Task AddGenreAsync(GenreCreateFormModel model);
		Task DeleteGenreAsync(int? id);
		Task EditGenreAsync(GenreEditFormModel model);
		Task<IEnumerable<GenreViewModel>> GetAllViewModelAsync();
		Task<GenreEditFormModel> GetEditModelByIdAsync(int? id);
		Task<GenreViewModel> GetViewModelByIdAsync(int? id);
	}
}
