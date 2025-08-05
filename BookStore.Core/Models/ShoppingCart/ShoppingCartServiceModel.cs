using BookStore.Core.Models.Book;

namespace BookStore.Core.Models.ShoppingCart
{
	public class ShoppingCartServiceModel
	{
		public int Id { get; set; }

		public int Count { get; set; }

		public int BookId { get; set; }
		public BookViewModel Book { get; set; } = null!;

		public string ApplicationUserId { get; set; } = null!;
	}
}
