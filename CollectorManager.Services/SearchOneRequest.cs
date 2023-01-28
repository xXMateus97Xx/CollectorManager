using CollectorManager.Data;
using System.Linq.Expressions;

namespace CollectorManager.Services;

public abstract class SearchOneRequest<TEntity> where TEntity : BaseEntity
{
    public int Id { get; set; }

    public virtual Expression<Func<TEntity, bool>> Filter => x => x.Id == Id;
}
