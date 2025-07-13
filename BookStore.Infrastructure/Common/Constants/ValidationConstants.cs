namespace BookStore.Infrastructure.Common.Constants
{
	public static class ValidationConstants
	{
		// Genre validation constants
		public const int GenreNameMinLength = 1;
		public const int GenreNameMaxLength = 20;

		public const int GenreDisplayOrderMinValue = 1;
		public const int GenreDisplayOrderMaxValue = 99;

		// Book Validation constants
		public const int AuthorMinLength = 1;
		public const int AuthorMaxLength = 55;

		public const int BookTitleMinLength = 1;
		public const int BookTitleMaxLength = 55;

		public const int BookDescriptionMinLength = 1;
		public const int BookDescriptionMaxLength = 525;

		public const int BookIsbnMinLength = 10;
		public const int BookIsbnMaxLength = 13;

		public const double BookMinPrice = 0;
		public const double BookMaxPrice = 1_000;
	}
}
