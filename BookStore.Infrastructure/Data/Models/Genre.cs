using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static BookStore.Infrastructure.Common.Constants.ValidationConstants;

namespace BookStore.Infrastructure.Data.Models
{
	[Comment("Genre of the book")]
	public class Genre
	{
		[Comment("Genre unique identifier")]
		[Key]
		public int Id { get; set; }

		[Comment("Genre name")]
		[Required]
		[MaxLength(GenreNameMaxLength)]
		public string Name { get; set; } = null!;

		[Comment("Represents a place when sorting in lists")]
		[Required]
		public int DisplayOrder { get; set; }
	}
}
