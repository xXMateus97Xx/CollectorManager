using CollectorManager.Data;
using CollectorManager.Data.Domains;

namespace CollectorManager.Services.Collections.DTOs;

public class CollectionResumeModel : BaseEntityModel
{
    public CollectionResumeModel()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }
    public CollectionType Type { get; set; }
    public int Items { get; set; }
}
