namespace BookStore.Infrastructure.Common.Messages
{
	public static class LoggMessages
	{
		public const string NullModelDetected = "Null '{ModelTypeName}' detected in '{ServiceName}' / '{MethodName}'";
		public const string ModelAlreadyExist = "Attempt to create existing '{ModelTypeName}' with name '{ModelName}' in '{ServiceName}' / '{MethodName}'";
		public const string DataModelSavingError = "An error occurred in '{ServiceName}' / '{MethodName}' while saving changes to DB!";
		public const string InvalidModelState = "Invalid ModelState for '{ModelTypeName}' in '{ControllerName}' / '{ActionName}'";
		public const string NullIdSelected = "Null ID selected in '{ServiceName}' / '{MethodName}'";
		public const string ModelIdNotFound = "'{ModelTypeName}' with ID - '{IdValue}' not found in '{ServiceName}' / '{MethodName}'";
		public const string ModelCreatedSuccessfully = "'{ModelTypeName}' '{ModelName}' created successfully!";
		public const string ModelUpdatedSuccessfully = "'{ModelTypeName}' '{ModelId}' updated successfully!";
		public const string ModelDeletedSuccessfully = "'{ModelTypeName}' '{ModelName}' deleted successfully!";
	}
}
