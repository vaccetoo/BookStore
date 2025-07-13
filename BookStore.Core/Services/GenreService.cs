using BookStore.Core.Contracts;
using BookStore.Infrastructure.Common.Contracts;

namespace BookStore.Core.Services
{
	public class GenreService : IGenreService
	{
		private readonly IUnitOfWork _unitOfWork;

		public GenreService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	}
}
