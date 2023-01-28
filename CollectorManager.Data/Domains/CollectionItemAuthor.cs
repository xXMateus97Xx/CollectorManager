namespace CollectorManager.Data.Domains;

public class CollectionItemAuthor : BaseEntity
{
	public int CollectionItemId { get; set; }

	private CollectionItem? _collectionItem;
	public CollectionItem CollectionItem
	{
		get { return _collectionItem ?? throw new InvalidOperationException("CollectionItem not initialized"); }
		set { _collectionItem = value; }
	}

	public int AuthorId { get; set; }

	private CollectionAuthor? _collectionAuthor;
	public CollectionAuthor CollectionAuthor
	{
		get { return _collectionAuthor ?? throw new InvalidOperationException("CollectionAuthor not initialized"); }
		set { _collectionAuthor = value; }
	}
}
