using System;
using System.Threading.Tasks;

namespace Redbridge.Threading
{
	public static class TaskExtensions
	{
		public static void WaitAndIgnoreException(this Task task)
		{
			try
			{
				task.Wait();
			}
			catch
			{
				// ignored
			}
		}

		public static void WaitAndUnwrapException(this Task task)
		{
			try
			{
				task.Wait();
			}
			catch (AggregateException ae)
			{
				throw ae.GetBaseException();
			}
		}

		public static T ResultOrUnwrapException<T>(this Task<T> task)
		{
			try
			{
				return task.Result;
			}
			catch (AggregateException ae)
			{
				throw ae.GetBaseException();
			}
		}

		public static void AssertSuccess(this Task task, string message = "")
		{
			task.WaitAndUnwrapException();
		}

		public static T AssertSuccess<T>(this Task<T> task, string message = "")
		{
			try
			{
				return task.Result;
			}
			catch (AggregateException ae)
			{
				throw ae.GetBaseException();
			}
		}
	}
}
