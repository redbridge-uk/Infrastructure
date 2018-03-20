﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Redbridge.Data.Mapping
{
	public interface IDataReader : IDisposable
	{
		IEnumerable<TRecord> ReadAll<TRecord>(Stream stream, FieldMappingCollection<TRecord> fieldMappings) where TRecord : new();
	}

	public interface IRecordStreamWriter : IDisposable
	{
		Task<Stream> WriteAsync<TRecord>(IEnumerable<TRecord> records, FieldMappingCollection<TRecord> fieldDefinitions) where TRecord : new();
	}

	public interface IDataParserFactory
	{
		IDataReader GetDataReader(string fileType);
		IRecordStreamWriter GetDataWriter(string fileType);
	}
}
