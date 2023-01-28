using CollectorManager.Data;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace CollectorManager.Services;

public abstract class UpdateEntityRequest<TEntity> : BaseEntityModel
    where TEntity : BaseEntity
{
    public abstract ValueTask<ValidationResult> ValidateAsync(CancellationToken cancellationToken);

    public async ValueTask<bool> IsValidAsync(CancellationToken cancellationToken) => (await ValidateAsync(cancellationToken)).IsValid;

    public virtual Expression<Func<TEntity, bool>> UpdateFilter => x => x.Id == Id;

    public virtual IQueryable<TEntity> UpdateInclude(IQueryable<TEntity> query) => query;
}
