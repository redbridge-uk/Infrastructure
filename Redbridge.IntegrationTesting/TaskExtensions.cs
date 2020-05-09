using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Redbridge.Exceptions;
using Redbridge.Threading;

namespace Redbridge.IntegrationTesting
{
	public static class TaskExtensions
	{
		public static T AssertResult<T>(this Task<T> task, Func<T, bool> resultFunc, string message = "Unexpected result from comparison function.")
		{
			try
			{
				var result = task.Result;
				Assert.IsTrue(resultFunc(result), message);
				return result;
			}
			catch (AggregateException ae)
			{
				throw ae.GetBaseException();
			}
		}

		public static void ExpectUserNotAuthenticatedException(this Task task)
		{
			try
			{
				task.WaitAndUnwrapException();
				Assert.Fail("Expected this call to throw a security exception.");
			}
			catch (UserNotAuthenticatedException)
			{

			}
			catch (Exception e)
			{
				Assert.Fail("Expected this call to throw a UserNotAuthenticatedException instead we received a {0}", e.GetBaseException().GetType().Name);
			}
		}

		public static void ExpectUserNotAuthenticatedException<T>(this Task<T> task)
		{
			try
			{
				task.WaitAndUnwrapException();
				Assert.Fail("Expected this call to throw a security exception.");
			}
			catch (UserNotAuthenticatedException)
			{

			}
		}

        public static void ExpectUserNotAuthorisedException(this Task task)
        {
            try
            {
                task.WaitAndUnwrapException();
                Assert.Fail("Expected this call to throw a security exception.");
            }
            catch (UserNotAuthorizedException)
            {

            }
        }

		public static void ExpectUserNotAuthorisedException<T>(this Task<T> task)
		{
			try
			{
				task.WaitAndUnwrapException();
				Assert.Fail("Expected this call to throw a security exception.");
			}
			catch (UserNotAuthorizedException)
			{

			}
		}

		public static void ExpectValidationException(this Task task)
		{
			try
			{
				task.WaitAndUnwrapException();
				Assert.Fail("Expected this call to throw a validation exception.");
			}
			catch (ValidationException)
			{

			}
		}

		public static void ExpectValidationException(this Task task, string message)
		{
			try
			{
				task.WaitAndUnwrapException();
				Assert.Fail("Expected this call to throw a validation exception.");
			}
			catch (ValidationException ve)
			{
				Assert.AreEqual(ve.Message, message, "Expected message: {0} received {1}", message, ve.Message);
			}
		}

		public static void ExpectValidationException<T>(this Task<T> task)
		{
			try
			{
				task.WaitAndUnwrapException();
				Assert.Fail("Expected this call to throw a validation exception.");
			}
			catch (ValidationException)
			{

			}
		}

		public static void ExpectValidationException<T>(this Task<T> task, string message)
		{
			try
			{
				task.WaitAndUnwrapException();
				Assert.Fail("Expected this call to throw a validation exception.");
			}
			catch (ValidationException ve)
			{
				Assert.AreEqual(message, ve.Message);
			}
		}
	}
}
