using BookStore.Core.Contracts;
using BookStore.Core.Models.Genre;
using BookStore.Infrastructure.Common.Contracts;
using BookStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Services
{
	public class GenreService : IGenreService
	{
		private readonly IUnitOfWork _unitOfWork;

		public GenreService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<GenreViewModel>> AllGenresAsync()
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
	}
}
