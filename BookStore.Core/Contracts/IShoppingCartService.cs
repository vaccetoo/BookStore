using BookStore.Core.Models.Book;

namespace BookStore.Core.Contracts
{
	public interface IShoppingCartService
	{
		Task AddToCartAsync(int bookId, int count, string userId);
	}
}
