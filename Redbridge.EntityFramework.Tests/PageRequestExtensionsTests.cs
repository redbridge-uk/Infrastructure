using System;
using System.Linq;
using NUnit.Framework;
using Redbridge.EntityFramework;
using Redbridge.SDK;
using Redbridge.Linq;

namespace Easilog.Data.EntityFramework.Tests
{
	[TestFixture]
	public class PageRequestExtensionsTests
	{
		public class ClassWithCreated
		{
			public ClassWithCreated()
			{
				Created = DateTime.Now;
			}

			public DateTime Created { get; set; }
		}

		[TestCase]
		public void PagedResultSetExecuteAsyncExpectSuccessCheckItemsCount()
		{
			var request = PageRequest.First;
			request.Sort = null;
			var list = new InMemoryDbSet<string> { "a", "b", "c" };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(3, result.Items.Count());
		}

		[TestCase]
		public void PagedResultSetExecuteAsyncExpectSuccessCheckTotalItemsCount()
		{
			var request = PageRequest.First;
			request.Sort = null;
			var list = new InMemoryDbSet<string> { "a", "b", "c" };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(3, result.TotalItems);
		}

		[TestCase]
		public void PagedResultSetExecuteAsyncExpectSuccessCheckTotalPages()
		{
			var request = PageRequest.First;
			request.Sort = null;
			var list = new InMemoryDbSet<string> { "a", "b", "c" };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(1, result.TotalPages);
		}

		[TestCase]
		public void FirstPagePagedResultSetSortedExecuteAsyncExpectSuccessCheckItemsCount()
		{
			var request = PageRequest.First;
			var list = new InMemoryDbSet<ClassWithCreated> { new ClassWithCreated(), new ClassWithCreated(), new ClassWithCreated() };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(3, result.Items.Count());
		}

		[TestCase]
		public void FirstPagePagedResultSetSortedExecuteAsyncExpectSuccessCheckTotalItemsCount()
		{
			var request = PageRequest.First;
			var list = new InMemoryDbSet<ClassWithCreated> { new ClassWithCreated(), new ClassWithCreated(), new ClassWithCreated() };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(3, result.TotalItems);
		}

		[TestCase]
		public void FirstPagePagedResultSetSortedExecuteAsyncExpectSuccessCheckTotalPages()
		{
			var request = PageRequest.First;
			var list = new InMemoryDbSet<ClassWithCreated> { new ClassWithCreated(), new ClassWithCreated(), new ClassWithCreated() };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(1, result.TotalPages);
		}

		[TestCase]
		public void PagePagedResultSetSortedExecuteAsyncExpectSuccessCheckItemsCount()
		{
			var request = PageRequest.Create(20, 1, new[] { new PageSort(1, SortDirection.Ascending, "Created") });
			var list = new InMemoryDbSet<ClassWithCreated> { new ClassWithCreated(), new ClassWithCreated(), new ClassWithCreated() };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(3, result.Items.Count());
		}

		[TestCase]
		public void PagedResultSetSortedExecuteAsyncExpectSuccessCheckTotalItemsCount()
		{
			var request = PageRequest.Create(20, 1, new[] { new PageSort(1, SortDirection.Ascending, "Created") });
			var list = new InMemoryDbSet<ClassWithCreated> { new ClassWithCreated(), new ClassWithCreated(), new ClassWithCreated() };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(3, result.TotalItems);
		}

		[TestCase]
		public void PagedResultSetSortedExecuteAsyncExpectSuccessCheckTotalPages()
		{
			var request = PageRequest.Create(20, 1, new[] { new PageSort(1, SortDirection.Ascending, "Created") });
			var list = new InMemoryDbSet<ClassWithCreated> { new ClassWithCreated(), new ClassWithCreated(), new ClassWithCreated() };
			var result = request.ExecuteAsync(list).Result;
			Assert.AreEqual(1, result.TotalPages);
		}
	}
}
