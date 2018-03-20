using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Redbridge.IO;

namespace Redbridge.Xml
{
	public class GenericSerializer<T>
	{
		private readonly XmlSerializer _xmlSerialiser;

		public GenericSerializer() 
        {
            _xmlSerialiser = new XmlSerializer(typeof(T));
        }

		public GenericSerializer(string defaultNamespace)
		{
			if (!string.IsNullOrEmpty(defaultNamespace))
			{
				_xmlSerialiser = new XmlSerializer(typeof(T), defaultNamespace);
			}
			else
			{
				_xmlSerialiser = new XmlSerializer(typeof(T));
			}
		}

        public GenericSerializer (XmlRootAttribute root)
        {
            _xmlSerialiser = new XmlSerializer(typeof(T), root);
        }

		public string Serialize(T item)
		{
			using (var stringWriter = new Utf8StringWriter())
			{
				_xmlSerialiser.Serialize(stringWriter, item);

				return stringWriter.ToString();
			}
		}

		public XmlElement ToElement(T item)
		{
			using (var stringWriter = new Utf8StringWriter())
			{
				_xmlSerialiser.Serialize(stringWriter, item);
				XmlDocument document = new XmlDocument();
				document.LoadXml(stringWriter.ToString());
				return document.DocumentElement;
			}
		}

		public T Deserialize(string serialisedItem)
		{
			try
			{
				using (StringReader stringReader = new StringReader(serialisedItem))
				{
					return (T)_xmlSerialiser.Deserialize(stringReader);
				}
			}
			catch (ArgumentNullException ane)
			{
				throw new XmlSerializationException("Unable to deserialize the supplied parameter as its content is null.", ane);
			}
			catch (InvalidOperationException ioe)
			{
				throw new XmlSerializationException("Unable to deserialize the supplied parameter as its content is empty or corrupt.", ioe);
			}
		}

		public T Deserialize(XmlElement element)
		{
			using (StringReader reader = new StringReader(element.OuterXml))
			{
				return (T)_xmlSerialiser.Deserialize(reader);
			}
		}

		public T Deserialize(Stream stream)
		{
			return (T)_xmlSerialiser.Deserialize(stream);
		}

		public T Deserialize(XmlReader reader)
		{
			return (T)_xmlSerialiser.Deserialize(reader);
		}

		public T Deserialize(XmlReader reader, string encodingStyle)
		{
			return (T)_xmlSerialiser.Deserialize(reader, encodingStyle);
		}

		public T Deserialize(TextReader serialisedItemTextReader)
		{
			return (T)_xmlSerialiser.Deserialize(serialisedItemTextReader);
		}

		public T FromFile(string filePath)
		{
			using (TextReader reader = File.OpenText(filePath))
			{
				return Deserialize(reader);
			}
		}
	}
}
