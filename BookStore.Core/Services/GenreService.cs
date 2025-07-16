using BookStore.Core.Contracts;
using BookStore.Core.Models.Genre;
using BookStore.Infrastructure.Common.Contracts;
using BookStore.Infrastructure.Common.Messages;
using BookStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static BookStore.Infrastructure.Common.Messages.LoggMessages;

namespace BookStore.Core.Services
{
	public class GenreService : IGenreService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<GenreService> _logger;

		public GenreService(IUnitOfWork unitOfWork,
			ILogger<GenreService> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task AddGenreAsync(GenreCreateFormModel model)
		{
			if (model == null)
			{
				_logger.LogWarning(NullModelDetected,
					nameof(GenreCreateFormModel),
					nameof(GenreService),
					nameof(AddGenreAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidModelSubmitted,
					nameof(Genre)));
			}

			bool genreExist = await _unitOfWork
				.AllAsNoTracking<Genre>()
				.AnyAsync(g => g.Name.Trim().ToLower() == model.Name.Trim().ToLower());

			if (genreExist)
			{
				_logger.LogWarning(LoggMessages.ModelAlreadyExist,
					nameof(Genre),
					model.Name,
					nameof(GenreService),
					nameof(AddGenreAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.AlreadyExist,
					nameof(Genre),
					model.Name));
			}

			Genre entity = new Genre()
			{
				Name = model.Name,
				DisplayOrder = model.DisplayOrder
			};

			try
			{
				await _unitOfWork.AddAsync<Genre>(entity);
				await _unitOfWork.SaveChangesAsync();

				_logger.LogInformation(ModelCreatedSuccessfully,
					nameof(Genre),
					entity.Name);
			}
			catch (Exception ex)
			{
				_logger.LogError(DataModelSavingError,
					nameof(GenreService),
					nameof(AddGenreAsync));

				throw new Exception(ExceptionMessages.UnexpextedError, ex);
			}
		}

		public async Task DeleteGenreAsync(int? id)
		{
			if (id == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(GenreService),
					nameof(DeleteGenreAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Genre)));
			}

			Genre? entity = await _unitOfWork.GetByIdAsync<Genre>(id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Genre),
					id.ToString(),
					nameof(GenreService),
					nameof(DeleteGenreAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Genre)));
			}

			await _unitOfWork.DeleteAsync<Genre>(id);
			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation(LoggMessages.ModelDeletedSuccessfully,
				nameof(Genre),
				entity.Name);
		}

		public async Task EditGenreAsync(GenreEditFormModel model)
		{
			if (model == null)
			{
				_logger.LogWarning(LoggMessages.NullModelDetected,
					nameof(GenreEditFormModel),
					nameof(GenreService),
					nameof(EditGenreAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Genre)));
			}

			bool genreExist = await _unitOfWork
				.AllAsNoTracking<Genre>()
				.AnyAsync(g =>
				g.Id != model.Id &&
				g.Name.Trim().ToLower() == model.Name.Trim().ToLower());

			if (genreExist)
			{
				_logger.LogWarning(LoggMessages.ModelAlreadyExist,
					nameof(Genre),
					model.Name,
					nameof(GenreService),
					nameof(EditGenreAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.AlreadyExist,
					nameof(Genre),
					model.Name));
			}

			Genre? entity = await _unitOfWork.GetByIdAsync<Genre>(model.Id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Genre),
					model.Id.ToString(),
					nameof(GenreService),
					nameof(EditGenreAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Genre)));
			}

			entity.Id = model.Id;
			entity.Name = model.Name;
			entity.DisplayOrder = model.DisplayOrder;

			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation(LoggMessages.ModelUpdatedSuccessfully,
				nameof(Genre),
				model.Id.ToString());
		}

		public async Task<IEnumerable<GenreViewModel>> GetAllViewModelAsync()
		{
			return await _unitOfWork.AllAsNoTracking<Genre>()
				.Select(g => new GenreViewModel()
				{
					Id = g.Id,
					Name = g.Name,
					DisplayOrder = g.DisplayOrder
				})
				.ToListAsync();
		}

		public async Task<GenreEditFormModel> GetEditModelByIdAsync(int? id)
		{
			if (id == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(GenreService),
					nameof(GetEditModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Genre)));
			}

			Genre? entity = await _unitOfWork.GetByIdAsync<Genre>(id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Genre),
					id.ToString(),
					nameof(GenreService),
					nameof(GetEditModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Genre)));
			}

			return new GenreEditFormModel
			{
				Id = entity.Id,
				Name = entity.Name,
				DisplayOrder = entity.DisplayOrder
			};
		}

		public async Task<GenreViewModel> GetViewModelByIdAsync(int? id)
		{
			if (id == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(GenreService),
					nameof(GetViewModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Genre)));
			}

			Genre? entity = await _unitOfWork.GetByIdAsync<Genre>(id);

			if (entity == null)
			{
				_logger.LogWarning(LoggMessages.ModelIdNotFound,
					nameof(Genre),
					id.ToString(),
					nameof(GenreService),
					nameof(GetViewModelByIdAsync));

				throw new InvalidOperationException(string.Format(ExceptionMessages.NotFound,
					nameof(Genre)));
			}

			return new GenreViewModel
			{
				Id = entity.Id,
				Name = entity.Name,
				DisplayOrder = entity.DisplayOrder
			};
		}
	}
}
