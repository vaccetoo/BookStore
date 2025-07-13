namespace BookStore.Infrastructure.Common.Messages
{
	public static class ConstraintUserMessages
	{
		public const string RequiredMessage = "The {0} field is required!";
		public const string StringLengthMessage = "The {0} field must be between {2} and {1} symbols!";
		public const string RangeMessage = "The {0} field must be in range {1} to {2}!";
	}
}
