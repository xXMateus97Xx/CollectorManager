namespace CollectorManager.Data.Domains;

public class CollectionItem : BaseEntity
{
    public CollectionItem()
    {
        Name = string.Empty;
        ItemAuthors = new HashSet<CollectionItemAuthor>();
    }

    public string Name { get; set; }

    public ICollection<CollectionItemAuthor> ItemAuthors { get; set; }

    public int Quantity { get; set; }

    public int FormatId { get; set; }

    private CollectionFormat? _format;
    public CollectionFormat Format
    {
        get => _format ?? throw new InvalidOperationException("Format not initialized");
        set => _format = value;
    }

    public int CollectionId { get; set; }

    private Collection? _collection;
    public Collection Collection
    {
        get => _collection ?? throw new InvalidOperationException("Collection not initialized");
        set => _collection = value;
    }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdateAt { get; set; }
}
