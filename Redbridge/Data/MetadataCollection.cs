using System.Collections.ObjectModel;

namespace Redbridge.Data
{
public class MetadataCollection : KeyedCollection<string, IMetadata>
{
	public MetadataCollection() { }

	protected override string GetKeyForItem(IMetadata item)
	{
		return item.Id;
	} }
}
