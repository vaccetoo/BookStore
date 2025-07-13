using BookStore.Core.Models.Genre;

namespace BookStore.Core.Contracts
{
	public interface IGenreService
	{
		Task<IEnumerable<GenreViewModel>> AllGenresAsync();
	}
}
