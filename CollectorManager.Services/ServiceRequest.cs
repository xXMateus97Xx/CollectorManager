using CollectorManager.Data;
using FluentValidation.Results;

namespace CollectorManager.Services;

public abstract class ServiceRequest : BaseEntityModel
{
    public abstract ValueTask<ValidationResult> ValidateAsync(CancellationToken cancellationToken);

    public async ValueTask<bool> IsValidAsync(CancellationToken cancellationToken) => (await ValidateAsync(cancellationToken)).IsValid;
}
