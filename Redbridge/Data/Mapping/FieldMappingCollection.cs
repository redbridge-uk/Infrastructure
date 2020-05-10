using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Redbridge.Data.Mapping
{
	public class FieldMappingCollection<TRecord> : KeyedCollection<string, IFieldMap<TRecord>> where TRecord : new()
	{
		private readonly IDataParserFactory _factory;

		public FieldMappingCollection(IDataParserFactory factory)
		{
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
		}

		protected override string GetKeyForItem(IFieldMap<TRecord> item)
		{
			return item.FieldName;
		}

		public IEnumerable<TRecord> Read(Stream fileStream, string fileName)
		{
			if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));
			if (fileName == null) throw new ArgumentNullException(nameof(fileName));

			var dataReader = _factory.GetDataReader(Path.GetExtension(fileName)
				.Replace(".", string.Empty));
			var records = dataReader.ReadAll<TRecord>(fileStream, this);

			var recordsArray = records.ToArray();
			recordsArray.ForEach(OnReadRecord);
			return recordsArray;
		}

		protected virtual void OnReadRecord(TRecord obj)
		{
		}

		public async Task<Stream> Write(IEnumerable<TRecord> records, string extension)
		{
			if (records == null) throw new ArgumentNullException(nameof(records));

			var dataReader = _factory.GetDataWriter(extension);
			var stream = await dataReader.WriteAsync<TRecord>(records, this);

			return stream;
		}
	}
}
