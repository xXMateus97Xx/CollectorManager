using CollectorManager.Data;

namespace CollectorManager.Services.CollectionAuthors.DTOs;

public class AuthorResumeModel : BaseEntityModel
{
    public AuthorResumeModel()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }
}
