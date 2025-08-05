using BookStore.Core.Models.Book;
using BookStore.Core.Models.ShoppingCart;

namespace BookStore.Core.Contracts
{
	public interface IShoppingCartService
	{
		Task AddToCartAsync(int bookId, int count, string userId);
		Task<ShoppingCartViewModel> GetShoppingCartByUserIdAsync(string? userId);
		Task IncreaseCountAsync(int? cartId);
		Task ReduceCountAsync(int? cartId);
		Task RemoveItemAsync(int? cartId);
	}
}
