using BookStore.Core.Contracts;
using BookStore.Core.Models.Book;
using BookStore.Core.Models.ShoppingCart;
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

				throw new InvalidOperationException(ExceptionMessages.UserDoesNotExist);
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

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
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

		public async Task<ShoppingCartViewModel> GetShoppingCartByUserIdAsync(string? userId)
		{
			if (userId == null)
			{
				_logger.LogWarning(NullIdSelected,
					nameof(ShoppingCartService),
					nameof(GetShoppingCartByUserIdAsync));

				throw new InvalidOperationException(ExceptionMessages.UserDoesNotExist);
			}

			var allCarts = await _unitOfWork.AllAsNoTracking<ShoppingCart>()
				.Where(sc => sc.ApplicationUserId == userId)
				.Include(sc => sc.Book)
				.ThenInclude(b => b.Genre)
				.Select(sc => new ShoppingCartServiceModel()
				{
					Id = sc.Id,
					BookId = sc.BookId,
					Count = sc.Count,
					ApplicationUserId = sc.ApplicationUserId,
					Book = new BookViewModel
					{
						Id = sc.Book.Id,
						Author = sc.Book.Author,
						Title = sc.Book.Title,
						Description = sc.Book.Description,
						ISBN = sc.Book.ISBN,
						Price = sc.Book.Price,
						ImageUrl = sc.Book.ImageUrl,
						Genre = sc.Book.Genre.Name,
						GenreId = sc.Book.GenreId
					}
				})
				.ToListAsync();

			var totalPrice = allCarts.Sum(x => x.Book.Price * x.Count);


			return new ShoppingCartViewModel
			{
				ShoppingCartList = allCarts,
				TotalPrice = totalPrice
			};
		}

		public async Task IncreaseCountAsync(int? cartId)
		{
			if (cartId == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(ShoppingCartService),
					nameof(IncreaseCountAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(ShoppingCart)));
			}

			var entity = await _unitOfWork.GetByIdAsync<ShoppingCart>(cartId);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(ShoppingCart),
					cartId.ToString(),
					nameof(ShoppingCartService),
					nameof(IncreaseCountAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(ShoppingCart)));
			}

			entity.Count++;

			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation(LoggMessages.CartCountUpdated);
		}

		public async Task ReduceCountAsync(int? cartId)
		{
			if (cartId == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(ShoppingCartService),
					nameof(ReduceCountAsync));

				throw new InvalidOperationException(
					string.Format(ExceptionMessages.NotFound, nameof(ShoppingCart)));
			}

			var entity = await _unitOfWork.GetByIdAsync<ShoppingCart>(cartId);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(ShoppingCart),
					cartId.ToString(),
					nameof(ShoppingCartService),
					nameof(ReduceCountAsync));

				throw new InvalidOperationException(
					string.Format(ExceptionMessages.NotFound, nameof(ShoppingCart)));
			}

			if (entity.Count > 1)
			{
				entity.Count--;
				_logger.LogInformation(LoggMessages.CartCountUpdated);
			}
			else
			{
				await _unitOfWork.DeleteAsync<ShoppingCart>(cartId);
				_logger.LogInformation(LoggMessages.CartItemDeleted);
			}

			await _unitOfWork.SaveChangesAsync();
		}

		public async Task RemoveItemAsync(int? cartId)
		{
			if (cartId == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(ShoppingCartService),
					nameof(RemoveItemAsync));

				throw new InvalidOperationException(
					string.Format(ExceptionMessages.NotFound, nameof(ShoppingCart)));
			}

			if (await _unitOfWork.GetByIdAsync<ShoppingCart>(cartId) == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(ShoppingCart),
					cartId.ToString(),
					nameof(ShoppingCartService),
					nameof(ReduceCountAsync));

				throw new InvalidOperationException(
					string.Format(ExceptionMessages.NotFound, nameof(ShoppingCart)));
			}

			await _unitOfWork.DeleteAsync<ShoppingCart>(cartId);
			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation(LoggMessages.CartItemDeleted);
		}
	}
}
