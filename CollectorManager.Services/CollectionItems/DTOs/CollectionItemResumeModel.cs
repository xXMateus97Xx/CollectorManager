using CollectorManager.Data;

namespace CollectorManager.Services.CollectionItems.DTOs;

public class CollectionItemResumeModel : BaseEntityModel
{
    public CollectionItemResumeModel()
    {
        Name = string.Empty;
        Authors = new List<string>();
        FormatName = string.Empty;
    }

    public string Name { get; set; }

    public List<string> Authors { get; set; }

    public int Quantity { get; set; }

    public string FormatName { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdateAt { get; set; }
}
