using BookStore.Core.Contracts;
using BookStore.Core.Models.Book;
using BookStore.Infrastructure.Common.Contracts;
using BookStore.Infrastructure.Common.Messages;
using BookStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static BookStore.Infrastructure.Common.Messages.LoggMessages;

namespace BookStore.Core.Services
{
	public class ShoppingCartService : IShoppingCartService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<ShoppingCartService> _logger;

		public ShoppingCartService(IUnitOfWork unitOfWork,
			ILogger<ShoppingCartService> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task AddToCartAsync(int bookId, int count, string? userId)
		{
			if (userId == null)
			{
				_logger.LogWarning(NullIdSelected, 
					nameof(ShoppingCartService), 
					nameof(AddToCartAsync));

				throw new ArgumentNullException(ExceptionMessages.UserDoesNotExist);
			}

			if (count < 1)
			{
				throw new ArgumentException(ExceptionMessages.InvalidCount);
			}

			var book = await _unitOfWork.All<Book>()
				.FirstOrDefaultAsync(b => b.Id == bookId);

			if (book == null)
			{
				_logger.LogWarning(NullIdSelected, 
					nameof(ShoppingCartService), 
					nameof(AddToCartAsync));

				throw new ArgumentNullException(string.Format(ExceptionMessages.NotFound, 
					nameof(Book)));
			}

			var existingCart = await _unitOfWork
				.All<ShoppingCart>()
				.FirstOrDefaultAsync(sc => sc.ApplicationUserId == userId && sc.BookId == bookId);

			if (existingCart != null)
			{
				existingCart.Count += count;
			}
			else
			{
				var cartItem = new ShoppingCart
				{
					ApplicationUserId = userId,
					BookId = bookId,
					Count = count
				};

				await _unitOfWork.AddAsync(cartItem);
			}

			try
			{
				await _unitOfWork.SaveChangesAsync();

				_logger.LogInformation(ModelCreatedSuccessfully, 
					nameof(ShoppingCart), 
					string.Empty);
			}
			catch (Exception ex)
			{
				_logger.LogError(DataModelSavingError, 
					nameof(ShoppingCartService), 
					nameof(AddToCartAsync));

				throw new Exception(ExceptionMessages.UnexpextedError, ex);
			}
		}
	}
}
