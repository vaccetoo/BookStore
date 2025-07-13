using System.ComponentModel.DataAnnotations;
using static BookStore.Infrastructure.Common.Messages.ConstraintUserMessages;
using static BookStore.Infrastructure.Common.Constants.ValidationConstants;

namespace BookStore.Core.Models.Genre
{
	public class GenreCreateFormModel
	{
		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(GenreNameMaxLength,
			MinimumLength = GenreNameMinLength,
			ErrorMessage = StringLengthMessage)]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[Range(GenreDisplayOrderMinValue, 
			GenreDisplayOrderMaxValue, 
			ErrorMessage = RangeMessage)]
		public int DisplayOrder { get; set; }
	}
}
