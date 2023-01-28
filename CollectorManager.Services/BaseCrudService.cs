using CollectorManager.Data;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using System.Linq.Expressions;
using System.Net;
using X.PagedList;

namespace CollectorManager.Services;

public abstract class BaseCrudService<TEntity, TResumeModel, TDetailsModel, TSearchRequest, TSearchOneRequest>
    : IBaseCrudService<TEntity, TResumeModel, TDetailsModel, TSearchRequest, TSearchOneRequest>
    where TEntity : BaseEntity
    where TResumeModel : BaseEntityModel
    where TDetailsModel : UpdateEntityRequest<TEntity>
    where TSearchRequest : PageableSearchRequest<TEntity>
    where TSearchOneRequest : SearchOneRequest<TEntity>
{
    private readonly IRepository<TEntity> _repository;
    private readonly IAppContext _appContext;

    public BaseCrudService(IRepository<TEntity> repository,
        IAppContext appContext)
    {
        _repository = repository;
        _appContext = appContext;
    }

    public async Task<ServiceResponse> InsertAsync(TDetailsModel model, CancellationToken cancellationToken)
    {
        await BeforeInsertValidationAsync(model, _appContext, cancellationToken);

        if (!await model.IsValidAsync(cancellationToken))
            return new ServiceResponse(HttpStatusCode.BadRequest);

        await BeforeInsertAsync(model, _appContext, cancellationToken);

        await _repository.InsertAsync(model, cancellationToken);

        return new ServiceResponse(HttpStatusCode.OK);
    }

    public async Task<ServiceResponse> UpdateAsync(TDetailsModel model, CancellationToken cancellationToken)
    {
        await BeforeUpdateValidationAsync(model, _appContext, cancellationToken);

        if (!await model.IsValidAsync(cancellationToken))
            return new ServiceResponse(HttpStatusCode.BadRequest);

        await BeforeUpdateAsync(model, _appContext, cancellationToken);

        await _repository.UpdateOneAsync(model, model.UpdateFilter, model.UpdateInclude, cancellationToken);

        return new ServiceResponse(HttpStatusCode.OK);
    }

    public async Task<ServiceResponse<List<T>>> GetAllAsync<T>(Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? orderBy, CancellationToken cancellationToken)
        where T : class
    {
        var result = await _repository.GetAllAsync<T>(filter, orderBy, cancellationToken);

        return new ServiceResponse<List<T>>(result);
    }

    public async Task<ServiceResponse<IPagedList<TResumeModel>>> GetAllAsync(TSearchRequest request, CancellationToken cancellationToken)
    {
        await BeforeSearchAsync(request, _appContext, cancellationToken);

        var list = await _repository.GetAllAsync<TResumeModel>(request.PageableSearch, cancellationToken);

        return new ServiceResponse<IPagedList<TResumeModel>>(list);
    }

    public async Task<ServiceResponse<TDetailsModel?>> GetByIdAsync(TSearchOneRequest request, CancellationToken cancellationToken)
    {
        await BeforeSearchByIdAsync(request, _appContext, cancellationToken);

        var details = await _repository.FirstOrDefaultAsync<TDetailsModel>(request.Filter, cancellationToken);
        if (details is null)
            return new ServiceResponse<TDetailsModel?>(HttpStatusCode.NotFound);

        return new ServiceResponse<TDetailsModel?>(details);
    }

    public async Task<ServiceResponse> DeleteAsync(TSearchOneRequest request, CancellationToken cancellationToken)
    {
        await BeforeSearchByIdAsync(request, _appContext, cancellationToken);

        await _repository.DeleteOneAsync(request.Filter, cancellationToken);

        return new ServiceResponse(HttpStatusCode.OK);
    }

    protected virtual Task BeforeInsertValidationAsync(TDetailsModel model, IAppContext appContext, CancellationToken cancellationToken) => Task.CompletedTask;

    protected virtual Task BeforeInsertAsync(TDetailsModel model, IAppContext appContext, CancellationToken cancellationToken) => Task.CompletedTask;

    protected virtual Task BeforeUpdateValidationAsync(TDetailsModel model, IAppContext appContext, CancellationToken cancellationToken) => Task.CompletedTask;

    protected virtual Task BeforeUpdateAsync(TDetailsModel model, IAppContext appContext, CancellationToken cancellationToken) => Task.CompletedTask;

    protected virtual Task BeforeSearchAsync(TSearchRequest model, IAppContext appContext, CancellationToken cancellationToken) => Task.CompletedTask;

    protected virtual Task BeforeSearchByIdAsync(TSearchOneRequest request, IAppContext appContext, CancellationToken cancellationToken) => Task.CompletedTask;
}
