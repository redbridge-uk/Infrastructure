using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Redbridge.Console
{
	public abstract class MultiKeyedCollection<TKey1, TKey2, TItem> : IQueryable<TItem>
	{
		private class KeyCollection<TKey> : KeyedCollection<TKey, TItem>
		{
			private Func<TItem, TKey> _keyRetriever;

			public KeyCollection(Func<TItem, TKey> keyRetriever)
			{
				_keyRetriever = keyRetriever;
			}

			protected override TKey GetKeyForItem(TItem item)
			{
				return _keyRetriever(item);
			}
		}

		private KeyCollection<TKey1> _standardCollection;
		private KeyCollection<TKey2> _alternateCollection;
		private IQueryable<TItem> _querableInstance;


		protected MultiKeyedCollection()
		{
			_standardCollection = new KeyCollection<TKey1>(GetKey1ForItem);
			_alternateCollection = new KeyCollection<TKey2>(GetKey2ForItem);
			_querableInstance = _standardCollection.AsQueryable();
		}

		public void Add(TItem item)
		{
			lock (this)
			{
				_standardCollection.Add(item);
				_alternateCollection.Add(item);
			}
		}

		public void AddRange(IEnumerable<TItem> items)
		{
			foreach (TItem item in items)
			{
				Add(item);
			}
		}

		public int Count
		{
			get { return _standardCollection.Count; }
		}

		public bool TryGet(TKey1 key, out TItem item)
		{
			TKey1 processedKey = PreProcessKey1(key);
			item = default(TItem);

			if (_standardCollection.Contains(processedKey))
			{
				item = _standardCollection[processedKey];
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Method that returns whether the multikeyed collection contains the specified key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool Contains(TKey1 key)
		{
			return _standardCollection.Contains(PreProcessKey1(key));
		}

		/// <summary>
		/// Method that is available to pre-process the provided key. 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		protected virtual TKey1 PreProcessKey1(TKey1 key)
		{
			return key;
		}

		/// <summary>
		/// Method that is available to pre-process the provided key. 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		protected virtual TKey2 PreProcessKey2(TKey2 key)
		{
			return key;
		}

		/// <summary>
		/// Method that access the key item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TItem this[TKey1 key]
		{
			get
			{
				TKey1 processedKey = PreProcessKey1(key);

				if (_standardCollection.Contains(processedKey))
					return _standardCollection[processedKey];
				else
				{
					object keyObj = (object)key;
					return this[keyObj];
				}
			}
		}

		/// <summary>
		/// Method that access the key item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TItem this[object key]
		{
			get
			{
				TKey2 key2 = (TKey2)key;
				return _alternateCollection[PreProcessKey2(key2)];
			}
		}

		/// <summary>
		/// Method that returns the key for the specified item.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected abstract TKey1 GetKey1ForItem(TItem item);

		/// <summary>
		/// Method that returns the alternate key for the specified item.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected abstract TKey2 GetKey2ForItem(TItem item);

		/// <summary>
		/// Gets the enumerator for the collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<TItem> GetEnumerator()
		{
			return _querableInstance.GetEnumerator();
		}

		/// <summary>
		/// Gets the enumerator for the collection.
		/// </summary>
		/// <returns></returns>
		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _querableInstance.GetEnumerator();
		}

		/// <summary>
		/// Gets the queryable element type.
		/// </summary>
		public Type ElementType
		{
			get { return typeof(TItem); }
		}

		/// <summary>
		/// Gets the expression for the instance.
		/// </summary>
		public Expression Expression
		{
			get { return _querableInstance.Expression; }
		}

		/// <summary>
		/// Gets the provider for the query
		/// </summary>
		public IQueryProvider Provider
		{
			get { return _querableInstance.Provider; }
		}
	}
}
