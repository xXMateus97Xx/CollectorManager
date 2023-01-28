namespace CollectorManager.Data.Domains;

public class CollectionFormat : BaseEntity
{
	public CollectionFormat()
	{
		Name = string.Empty;
	}

	public string Name { get; set; }

	public int CollectionId { get; set; }

	private Collection? _collection;
	public Collection Collection
	{
		get => _collection ?? throw new InvalidOperationException("Collection not Initialized");
		set => _collection = value;
	}
}
