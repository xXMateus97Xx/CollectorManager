using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.CollectionAuthors.DTOs;
using FluentValidation;

namespace CollectorManager.Services.CollectionAuthors.Validators;

internal class AuthorModelValidator : AbstractValidator<AuthorModel>
{
    private readonly IRepository<Collection> _collectionRepository;
    private readonly IAppContext _appContext;

    public AuthorModelValidator(IRepository<Collection> collectionRepository,
        IAppContext appContext)
    {
        _collectionRepository = collectionRepository;
        _appContext = appContext;

        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CollectionId).NotEmpty()
                .MustAsync(BeCollectionOwnerAsync)
                .WithMessage("Coleção não pertence ao usuário atual");
    }

    public async Task<bool> BeCollectionOwnerAsync(int collectionId, CancellationToken cancellationToken)
    {
        var user = await _appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        return await _collectionRepository.AnyAsync(x => x.Id == collectionId && x.UserId == user.Id, cancellationToken);
    }
}
