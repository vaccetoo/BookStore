using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Infrastructure.Data.Models
{
	[Comment("Represents Shopping cart in the store")]
	public class ShoppingCart
	{
		[Comment("ShoppingCart unique identifier")]
		[Key]
		public int Id { get; set; }

		[Comment("It shows the count of the ordered books")]
		[Required]	
		public int Count { get; set; }

		[Comment("Book unique identifier")]
		[Required]
		public int BookId { get; set; }
		[ForeignKey(nameof(BookId))]
		public Book Book { get; set; } = null!;

		[Comment("ApplicationUser unique identifier")]
		[Required]
		public string ApplicationUserId { get; set; } = null!;
		[ForeignKey(nameof(ApplicationUserId))]
		public ApplicationUser ApplicationUser { get; set; } = null!;
	}
}
