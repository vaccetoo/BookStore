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
	public class BookService : IBookService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<BookService> _logger;

		public BookService(IUnitOfWork unitOfWork,
			ILogger<BookService> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task AddBookAsync(BookCreateFormModel model)
		{
			if (model == null)
			{
				_logger.LogWarning(NullModelDetected,
					nameof(BookCreateFormModel),
					nameof(BookService),
					nameof(AddBookAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidModelSubmitted,
					nameof(Book)));
			}

			bool bookExist = await _unitOfWork
				.AllAsNoTracking<Book>()
				.AnyAsync(b => b.Title.Trim().ToLower() == model.Title.Trim().ToLower());

			if (bookExist)
			{
				_logger.LogWarning(LoggMessages.ModelAlreadyExist,
					nameof(Book),
					model.Title,
					nameof(BookService),
					nameof(AddBookAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.AlreadyExist,
					nameof(Book),
					model.Title));
			}

			Book entity = new Book()
			{
				Title = model.Title,
				Author = model.Author,
				Description = model.Description,
				ISBN = model.ISBN,
				Price = model.Price,
				ImageUrl = model.ImageUrl,
				GenreId = model.GenreId
			};

			try
			{
				await _unitOfWork.AddAsync<Book>(entity);
				await _unitOfWork.SaveChangesAsync();

				_logger.LogInformation(ModelCreatedSuccessfully,
					nameof(Book),
					entity.Title);
			}
			catch (Exception ex)
			{
				_logger.LogError(DataModelSavingError,
					nameof(BookService),
					nameof(AddBookAsync));

				throw new Exception(ExceptionMessages.UnexpextedError, ex);
			}
		}

		public async Task DeleteBookAsync(int? id)
		{
			if (id == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(BookService),
					nameof(DeleteBookAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			Book? entity = await _unitOfWork.GetByIdAsync<Book>(id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Book),
					id.ToString(),
					nameof(BookService),
					nameof(DeleteBookAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			await _unitOfWork.DeleteAsync<Book>(id);
			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation(LoggMessages.ModelDeletedSuccessfully,
				nameof(Book),
				entity.Title);
		}

		public async Task EditBookAsync(BookEditFormModel model)
		{
			if (model == null)
			{
				_logger.LogWarning(LoggMessages.NullModelDetected,
					nameof(BookEditFormModel),
					nameof(BookService),
					nameof(EditBookAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			bool bookExist = await _unitOfWork
				.AllAsNoTracking<Book>()
				.AnyAsync(b =>
				b.Id != model.Id &&
				b.Title.Trim().ToLower() == model.Title.Trim().ToLower());

			if (bookExist)
			{
				_logger.LogWarning(LoggMessages.ModelAlreadyExist,
					nameof(Book),
					model.Title,
					nameof(BookService),
					nameof(EditBookAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.AlreadyExist,
					nameof(Book),
					model.Title));
			}

			Book? entity = await _unitOfWork.GetByIdAsync<Book>(model.Id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Book),
					model.Id.ToString(),
					nameof(BookService),
					nameof(EditBookAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			entity.Id = model.Id;
			entity.Title = model.Title;
			entity.Author = model.Author;
			entity.Description = model.Description;
			entity.Price = model.Price;
			entity.GenreId = model.GenreId;
			entity.ImageUrl = model.ImageUrl;
			entity.ISBN = model.ISBN;

			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation(LoggMessages.ModelUpdatedSuccessfully,
				nameof(Book),
				model.Id.ToString());
		}

		public async Task<IEnumerable<BookViewModel>> GetAllViewModelAsync()
		{
			return await _unitOfWork.AllAsNoTracking<Book>()
				.Select(b => new BookViewModel()
				{
					Id = b.Id,
					Title = b.Title,
					Author = b.Author,
					Description = b.Description,
					ISBN = b.ISBN,
					Price = b.Price,
					ImageUrl = b.ImageUrl,
					Genre = b.Genre.Name,
					GenreId = b.GenreId
				})
				.ToListAsync();
		}

		public async Task<BookDetailsViewModel> GetDetailsViewModelByIdAsync(int? id)
		{
			if (id == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(BookService),
					nameof(GetDetailsViewModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			Book? entity = await _unitOfWork
				.AllAsNoTracking<Book>()
				.Include(b => b.Genre)
				.FirstOrDefaultAsync(b => b.Id == id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Book),
					id.ToString(),
					nameof(BookService),
					nameof(GetDetailsViewModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			return new BookDetailsViewModel
			{
				Id = entity.Id,
				Title = entity.Title,
				Author = entity.Author,
				Description = entity.Description,
				ISBN = entity.ISBN,
				Price = entity.Price,
				ImageUrl = entity.ImageUrl,
				GenreId = entity.GenreId,
				Genre = entity.Genre.Name,
				Count = 1
			};
		}

		public async Task<BookEditFormModel> GetEditModelByIdAsync(int? id)
		{
			if (id == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(BookService),
					nameof(GetEditModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			Book? entity = await _unitOfWork.GetByIdAsync<Book>(id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Book),
					id.ToString(),
					nameof(BookService),
					nameof(GetEditModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			return new BookEditFormModel
			{
				Id = entity.Id,
				Title = entity.Title,
				Author = entity.Author,
				Description = entity.Description,
				ISBN = entity.ISBN,
				Price = entity.Price,
				ImageUrl = entity.ImageUrl,
				GenreId = entity.GenreId
			};
		}

		public async Task<BookViewModel> GetViewModelByIdAsync(int? id)
		{
			if (id == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(BookService),
					nameof(GetViewModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			Book? entity = await _unitOfWork
				.AllAsNoTracking<Book>()
				.Include(b => b.Genre)
				.FirstOrDefaultAsync(b => b.Id == id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Book),
					id.ToString(),
					nameof(BookService),
					nameof(GetViewModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Book)));
			}

			return new BookViewModel
			{
				Id = entity.Id,
				Title = entity.Title,
				Author = entity.Author,
				Description = entity.Description,
				ISBN = entity.ISBN,
				Price = entity.Price,
				ImageUrl = entity.ImageUrl,
				GenreId = entity.GenreId,
				Genre = entity.Genre.Name
			};
		}
	}
}
