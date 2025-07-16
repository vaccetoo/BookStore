using BookStore.Core.Models.Book;

namespace BookStore.Core.Contracts
{
	public interface IBookService
	{
		Task AddBookAsync(BookCreateFormModel model);
		Task DeleteBookAsync(int? id);
		Task EditBookAsync(BookEditFormModel model);
		Task<IEnumerable<BookViewModel>> GetAllViewModelAsync();
		Task<BookEditFormModel> GetEditModelByIdAsync(int? id);
		Task<BookViewModel> GetViewModelByIdAsync(int? id);
	}
}
