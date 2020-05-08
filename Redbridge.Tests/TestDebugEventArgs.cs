using NUnit.Framework;
using Redbridge.Diagnostics;

namespace Redbridge.Tests
{
	[TestFixture]
	public class TestDebugEventArgs
	{
		[Test]
		public void ConstructDebugEventArgs ()
		{
			var e = new DebugEventArgs("I am a debug message bringing bad tidings.");
			Assert.AreEqual("I am a debug message bringing bad tidings.", e.Message, "Unexpected debug message.");
		}
	}
}
