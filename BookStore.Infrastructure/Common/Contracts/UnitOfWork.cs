using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Common.Contracts
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Add entity to DbContext
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
		{
			await GetDbSet<TEntity>().AddAsync(entity);
		}

		/// <summary>
		/// Returns specific DBSet with change tracker
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <returns></returns>
		public IQueryable<TEntity> All<TEntity>() where TEntity : class
		{
			return GetDbSet<TEntity>();
		}

		/// <summary>
		/// Returns specific DBSet without change tracker
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <returns></returns>
		public IQueryable<TEntity> AllAsNoTracking<TEntity>() where TEntity : class
		{
			return GetDbSet<TEntity>().AsNoTracking();
		}

		/// <summary>
		/// Deletes specific entity
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task DeleteAsync<TEntity>(object id) where TEntity : class
		{
			TEntity? entity = await GetByIdAsync<TEntity>(id);

			if (entity != null)
			{
				GetDbSet<TEntity>().Remove(entity);
			}
		}

		/// <summary>
		/// Dispose context when needed
		/// </summary>
		public void Dispose()
		{
			_context.Dispose();
		}

		/// <summary>
		/// Returns specific entity by its id
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<TEntity?> GetByIdAsync<TEntity>(object id) where TEntity : class
		{
			return await GetDbSet<TEntity>().FindAsync(id);
		}

		/// <summary>
		/// Saves changes made
		/// </summary>
		/// <returns></returns>
		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Returns specific DbSet from context
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <returns></returns>
		private DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
		{
			return _context.Set<TEntity>();
		}
	}
}
