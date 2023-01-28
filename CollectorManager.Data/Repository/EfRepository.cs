using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;

namespace CollectorManager.Data.Repository;

internal class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly CollectorsDbContext _dbContext;
    private readonly IMapper _mapper;

    public EfRepository(CollectorsDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task<List<TReturn>> GetAllAsync<TReturn>(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IQueryable<T>>? orderBy, CancellationToken cancellationToken)
         where TReturn : class
    {
        var query = _dbContext.Set<T>()
            .AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        return query.ProjectTo<TReturn>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public Task<IPagedList<TModel>> GetAllAsync<TModel>(PageableSearch<T> search, CancellationToken cancellationToken)
        where TModel : class
    {
        var query = _dbContext.Set<T>()
            .AsQueryable();

        if (search.Filter != null)
            query = query.Where(search.Filter);

        query = search.OrderBy(query);

        return query.ProjectTo<TModel>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(search.PageIndex, search.PageSize, cancellationToken);
    }

    public Task<TModel?> FirstOrDefaultAsync<TModel>(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        return _dbContext.Set<T>()
            .AsQueryable()
            .Where(filter)
            .ProjectTo<TModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<TModel?> GetByIdAsync<TModel>(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Set<T>()
            .AsQueryable()
            .Where(x => x.Id == id)
            .ProjectTo<TModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        return _dbContext.Set<T>()
            .AsQueryable()
            .Where(filter)
            .AnyAsync(cancellationToken);
    }

    public Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        return _dbContext.Set<T>()
            .AsQueryable()
            .Where(filter)
            .CountAsync(cancellationToken);
    }

    public async Task InsertAsync<TModel>(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TModel, T>(model);

        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync<TModel>(TModel model, CancellationToken cancellationToken) where TModel : BaseEntityModel
    {
        return UpdateOneAsync(model, x => x.Id == model.Id, cancellationToken);
    }

    public async Task UpdateOneAsync<TModel>(TModel model, Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<T>()
            .FirstOrDefaultAsync(filter, cancellationToken);

        await UpdateOneAsync(model, entity, cancellationToken);
    }

    public async Task UpdateOneAsync<TModel>(TModel model, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>>? include, CancellationToken cancellationToken)
    {
        var query = _dbContext.Set<T>()
            .AsQueryable();

        if (include != null)
            query = include(query);

        var entity = await query.FirstOrDefaultAsync(filter, cancellationToken);

        await UpdateOneAsync(model, entity, cancellationToken);
    }

    private Task UpdateOneAsync<TModel>(TModel model, T? entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            return Task.CompletedTask;

        _mapper.Map(model, entity);

        _dbContext.Set<T>().Update(entity);

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return DeleteOneAsync(x => x.Id == id, cancellationToken);
    }

    public async Task DeleteOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<T>()
            .FirstOrDefaultAsync(filter, cancellationToken);
        if (entity == null)
            return;

        _dbContext.Set<T>().Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

}
