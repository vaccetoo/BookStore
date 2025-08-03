namespace BookStore.Infrastructure.Common.Messages
{
	public static class ExceptionMessages
	{
		public const string NotFound = "{0} not found!";
		public const string InvalidModelSubmitted = "Invalid {0} submitted!";
		public const string AlreadyExist = "{0} {1} already exist!";
		public const string UnexpextedError = "An unexpexted error occurred!";
		public const string UserDoesNotExist = "Invalid User!";
		public const string InvalidCount = "Count must be at least 1!";
	}
}
