using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Redbridge.Data.Mapping;

namespace Redbridge.IO
{
	public abstract class RecordStreamWriter : IRecordStreamWriter
	{
		protected RecordStreamWriter(Stream stream)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));
			Stream = stream;
		}

		public Stream Stream { get; }

		protected virtual void Dispose(bool disposing) { }

		public void Dispose()
		{
			Dispose(true);
		}

		public abstract Task<Stream> WriteAsync<TRecord>(IEnumerable<TRecord> records,
			FieldMappingCollection<TRecord> fieldDefinitions) where TRecord : new();

		public virtual void Close() {}
	}
}
