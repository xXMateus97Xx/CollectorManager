using System.Linq.Expressions;
using X.PagedList;

namespace CollectorManager.Data.Repository;

public interface IRepository<T> where T : BaseEntity
{
    Task<IPagedList<TModel>> GetAllAsync<TModel>(PageableSearch<T> search, CancellationToken cancellationToken) where TModel : class;

    Task<List<TReturn>> GetAllAsync<TReturn>(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IQueryable<T>>? orderBy, CancellationToken cancellationToken) where TReturn : class;

    Task<TModel?> FirstOrDefaultAsync<TModel>(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task<TModel?> GetByIdAsync<TModel>(int id, CancellationToken cancellationToken);

    Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task InsertAsync<TModel>(TModel model, CancellationToken cancellationToken);

    Task UpdateAsync<TModel>(TModel model, CancellationToken cancellationToken) where TModel : BaseEntityModel;

    Task UpdateOneAsync<TModel>(TModel model, Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task UpdateOneAsync<TModel>(TModel model, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>>? include, CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);

    Task DeleteOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
}
