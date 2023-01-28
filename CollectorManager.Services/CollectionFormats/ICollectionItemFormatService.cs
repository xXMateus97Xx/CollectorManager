using CollectorManager.Data.Domains;
using CollectorManager.Services.CollectionFormats.DTOs;

namespace CollectorManager.Services.CollectionFormats;

public interface ICollectionItemFormatService : IBaseCrudService<CollectionFormat, FormatResumeModel,
    FormatModel, FormatPageableSearchModel, FormatSearchOneModel>
{
}
