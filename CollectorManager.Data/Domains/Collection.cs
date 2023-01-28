using System.ComponentModel.DataAnnotations;

namespace CollectorManager.Data.Domains;

public class Collection : BaseEntity
{
    public Collection()
    {
        Name = string.Empty;
        Items = new HashSet<CollectionItem>();
    }

    public string Name { get; set; }
    public CollectionType Type { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<CollectionItem> Items { get; set; }
}

public enum CollectionType
{
    [Display(Name = "Música")]
    Music,

    [Display(Name = "Livros")]
    Books,

    [Display(Name = "Jogos")]
    Games
}
