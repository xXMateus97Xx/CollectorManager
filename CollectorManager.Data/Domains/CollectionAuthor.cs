namespace CollectorManager.Data.Domains;

public class CollectionAuthor : BaseEntity
{
    public CollectionAuthor()
    {
        Name = string.Empty;
        Items = new HashSet<CollectionItemAuthor>();
    }

    public string Name { get; set; }

    public int CollectionId { get; set; }

    private Collection? _collection;
    public Collection Collection
    {
        get => _collection ?? throw new InvalidOperationException("Collection not initialized");
        set => _collection = value;
    }

    public ICollection<CollectionItemAuthor> Items { get; set; }
}
