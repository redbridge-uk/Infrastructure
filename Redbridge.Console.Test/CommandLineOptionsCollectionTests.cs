using NUnit.Framework;

namespace Redbridge.Console.Test
{
	[TestFixture]
	public class CommandLineOptionsCollectionTest
	{
		/// <summary>
		/// Test Condition: Ensure the collection starts empty.
		/// Expected Result:
		/// </summary>
		[TestCase]
		public void CreateCommandLineOptionsCollection_EnsureEmpty()
		{
			CommandLineOptionsCollection collection = new CommandLineOptionsCollection();
			Assert.AreEqual(0, collection.Count, "Expected zero elements initially.");
		}

		/// <summary>
		/// Test Condition: Ensure the collection starts empty.
		/// Expected Result:
		/// </summary>
		[TestCase]
		public void CreateCommandLineOptionsCollection_AddSingleItem_CheckCount()
		{
			CommandLineOptionsCollection collection = new CommandLineOptionsCollection();
			collection.Add(new SwitchAttribute("EnableLogging") { ShortHandName = "log" });
			Assert.AreEqual(1, collection.Count, "Expected zero elements initially.");
		}

		/// <summary>
		/// Test Condition: Ensure the collection starts empty.
		/// Expected Result:
		/// </summary>
		[TestCase]
		public void CreateCommandLineOptionsCollection_AddSingleItem_RecallByLongName_UpperCase()
		{
			CommandLineOptionsCollection collection = new CommandLineOptionsCollection();
			collection.Add(new SwitchAttribute("EnableLogging") { ShortHandName = "log" });
			SwitchAttribute attribute = (SwitchAttribute)collection["ENABLELOGGING"];
			Assert.IsNotNull(attribute, "Should have recalled the attribute.");
		}

		/// <summary>
		/// Test Condition: Ensure the collection starts empty.
		/// Expected Result:
		/// </summary>
		[TestCase]
		public void CreateCommandLineOptionsCollection_AddSingleItem_RecallByLongName_LowerCase()
		{
			CommandLineOptionsCollection collection = new CommandLineOptionsCollection();
			collection.Add(new SwitchAttribute("EnableLogging") { ShortHandName = "log" });
			SwitchAttribute attribute = (SwitchAttribute)collection["enablelogging"];
			Assert.IsNotNull(attribute, "Should have recalled the attribute.");
		}

		/// <summary>
		/// Test Condition: Ensure the collection starts empty.
		/// Expected Result:
		/// </summary>
		[TestCase]
		public void CreateCommandLineOptionsCollection_AddSingleItem_RecallByShortName_UpperCase()
		{
			CommandLineOptionsCollection collection = new CommandLineOptionsCollection();
			collection.Add(new SwitchAttribute("EnableLogging") { ShortHandName = "log" });
			SwitchAttribute attribute = (SwitchAttribute)collection["LOG"];
			Assert.IsNotNull(attribute, "Should have recalled the attribute.");
		}

		/// <summary>
		/// Test Condition: Ensure the collection starts empty.
		/// Expected Result:
		/// </summary>
		[TestCase]
		public void CreateCommandLineOptionsCollection_AddSingleItem_RecallByShortName_LowerCase()
		{
			CommandLineOptionsCollection collection = new CommandLineOptionsCollection();
			collection.Add(new SwitchAttribute("EnableLogging") { ShortHandName = "LOG" });
			SwitchAttribute attribute = (SwitchAttribute)collection["log"];
			Assert.IsNotNull(attribute, "Should have recalled the attribute.");
		}
	}
}
