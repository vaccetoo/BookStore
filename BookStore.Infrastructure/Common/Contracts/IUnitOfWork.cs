namespace BookStore.Infrastructure.Common.Contracts
{
	public interface IUnitOfWork
	{
		IQueryable<TEntity> All<TEntity>() where TEntity : class;
		IQueryable<TEntity> AllAsNoTracking<TEntity>() where TEntity : class;
		Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
		Task<int> SaveChangesAsync();
		Task<TEntity?> GetByIdAsync<TEntity>(object id) where TEntity : class;
		Task DeleteAsync<TEntity>(object id) where TEntity : class;
	}
}
