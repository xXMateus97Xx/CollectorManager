using CollectorManager.Data;

namespace CollectorManager.Services.CollectionFormats.DTOs;

public class FormatResumeModel : BaseEntityModel
{
    public FormatResumeModel()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }
}
