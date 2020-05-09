//using NUnit.Framework;
//using Redbridge.Collections;
//namespace Tests
//{
//	[TestFixture]
//	public class MultiKeyedCollectionTest
//	{
//		public class StringIntegerKeyedObject
//		{
//			public StringIntegerKeyedObject(int id, string key, string value)
//			{
//				Id = id;
//				StringKey = key;
//				Value = value;
//			}

//			public int Id
//			{
//				get;
//				set;
//			}

//			public string StringKey
//			{
//				get;
//				set;
//			}

//			public string Value
//			{
//				get;
//				set;
//			}
//		}

//		public class StringIntegerKeyedCollection : MultiKeyedCollection<string, int, StringIntegerKeyedObject>
//		{
//			protected override string GetKey1ForItem(StringIntegerKeyedObject item)
//			{
//				return item.StringKey;
//			}

//			protected override int GetKey2ForItem(StringIntegerKeyedObject item)
//			{
//				return item.Id;
//			}
//		}

//		[TestCase]
//		public void CreateMultiKeyedCollectionInstance_StringIntegerTypes_ExpectSuccess()
//		{
//			new StringIntegerKeyedCollection();
//		}

//		[TestCase]
//		public void CreateMultiKeyedCollectionInstance_StringIntegerTypes_Check0Entries()
//		{
//			var collection = new StringIntegerKeyedCollection();
//			Assert.AreEqual(0, collection.Count);
//		}

//		[TestCase]
//		public void CreateMultiKeyedCollectionInstance_StringIntegerTypes_AddSingleItem_Check1Entries()
//		{
//			var collection = new StringIntegerKeyedCollection();
//			collection.Add(new StringIntegerKeyedObject(1, "A", "Value A"));
//			Assert.AreEqual(1, collection.Count);
//		}

//		[TestCase]
//		public void CreateMultiKeyedCollectionInstance_StringIntegerTypes_AddMultipleItems_Check5Entries()
//		{
//			var collection = new StringIntegerKeyedCollection();
//			collection.Add(new StringIntegerKeyedObject(1, "A", "Value A"));
//			collection.Add(new StringIntegerKeyedObject(2, "B", "Value B"));
//			collection.Add(new StringIntegerKeyedObject(3, "C", "Value C"));
//			collection.Add(new StringIntegerKeyedObject(4, "D", "Value D"));
//			collection.Add(new StringIntegerKeyedObject(5, "E", "Value E"));
//			Assert.AreEqual(5, collection.Count);
//		}

//		[TestCase]
//		public void CreateMultiKeyedCollectionInstance_StringIntegerTypes_IndexAccessor_ByPrimaryKey_ExpectSuccess()
//		{
//			var collection = new StringIntegerKeyedCollection();
//			collection.Add(new StringIntegerKeyedObject(1, "A", "Value A"));
//			collection.Add(new StringIntegerKeyedObject(2, "B", "Value B"));
//			collection.Add(new StringIntegerKeyedObject(3, "C", "Value C"));
//			collection.Add(new StringIntegerKeyedObject(4, "D", "Value D"));
//			collection.Add(new StringIntegerKeyedObject(5, "E", "Value E"));

//			Assert.AreEqual("Value B", collection[2].Value);
//		}

//		[TestCase]
//		public void CreateMultiKeyedCollectionInstance_StringIntegerTypes_IndexAccessor_BySecondaryKey_ExpectSuccess()
//		{
//			var collection = new StringIntegerKeyedCollection();
//			collection.Add(new StringIntegerKeyedObject(1, "A", "Value A"));
//			collection.Add(new StringIntegerKeyedObject(2, "B", "Value B"));
//			collection.Add(new StringIntegerKeyedObject(3, "C", "Value C"));
//			collection.Add(new StringIntegerKeyedObject(4, "D", "Value D"));
//			collection.Add(new StringIntegerKeyedObject(5, "E", "Value E"));

//			Assert.AreEqual("Value C", collection["C"].Value);
//		}
//	}
//}
