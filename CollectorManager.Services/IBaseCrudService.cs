using CollectorManager.Data;
using System.Linq.Expressions;
using X.PagedList;

namespace CollectorManager.Services;

public interface IBaseCrudService<TEntity, TResumeModel, TDetailsModel, TSearchRequest, TSearchOneRequest>
    where TEntity : BaseEntity
    where TResumeModel : BaseEntityModel
    where TDetailsModel : UpdateEntityRequest<TEntity>
    where TSearchRequest : PageableSearchRequest<TEntity>
    where TSearchOneRequest : SearchOneRequest<TEntity>
{
    Task<ServiceResponse> InsertAsync(TDetailsModel model, CancellationToken cancellationToken);
    Task<ServiceResponse> UpdateAsync(TDetailsModel model, CancellationToken cancellationToken);
    Task<ServiceResponse<IPagedList<TResumeModel>>> GetAllAsync(TSearchRequest request, CancellationToken cancellationToken);
    Task<ServiceResponse<List<T>>> GetAllAsync<T>(Expression<Func<TEntity, bool>>? filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? orderBy, CancellationToken cancellationToken) where T : class;
    Task<ServiceResponse<TDetailsModel?>> GetByIdAsync(TSearchOneRequest request, CancellationToken cancellationToken);
    Task<ServiceResponse> DeleteAsync(TSearchOneRequest request, CancellationToken cancellationToken);
}
