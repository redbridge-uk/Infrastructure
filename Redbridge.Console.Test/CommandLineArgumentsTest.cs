using System.Text;
using NUnit.Framework;

namespace Redbridge.Console.Test
{
	[TestFixture]
	public class CommandLineArgumentsTest
	{
		private class SingleSwitchOptionsTest
		{
			[Switch("EnableLog", Required = false)]
			public bool EnableLogging { get; set; }
		}

		private class SingleSwitchOptionsTestDefaultTrue
		{
			[Switch("EnableLog", Required = false, DefaultValue = true)]
			public bool EnableLogging { get; set; }
		}

		private class SingleParameterOptions
		{
			[StringParameter("Assembly", ShortHandName = "a", Required = true, HelpText = "The assembly path")]
			public string Assembly { get; set; }
		}

		private enum SummaryOptions
		{
			Normal,
			High,
			Verbose
		}

		private class EnumParameterOptions
		{
			[EnumParameter("Level", DefaultValue = SummaryOptions.Normal, Required = false, HelpText = "The logging level")]
			public SummaryOptions Level { get; set; }
		}

		[CommandLineOptions(HelpResource = "SomeText.txt")]
		private class HelpTextResourceDefinedClass
		{
		}

		[CommandLineOptions(BannerResource = "SomeBanner.txt")]
		private class BannerTextResourceDefinedClass
		{
		}

		private class ParameterArrayOptionsTestClass
		{
			public ParameterArrayOptionsTestClass()
			{
				Parameters = new ParameterCollection();
			}

			[ParameterCollection]
			public ParameterCollection Parameters { get; set; }
		}

		private class IntegerParameterOptionsTest
		{
			[IntegerParameter("Port", Required = false)]
			public int Port { get; set; }
		}

		private class DefaultIntegerParameterOptionsTest
		{
			[IntegerParameter("Port", Required = false, DefaultValue = 1234)]
			public int Port { get; set; }
		}

		[TestCase]
		public void ConstructCommandLineArguments_AssertSetup()
		{
			var arguments = CommandLineArguments<SingleSwitchOptionsTest>.Setup();
			Assert.IsNotNull(arguments);
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseEmpty_InstanceCreated()
		{
			CommandLineArguments<SingleSwitchOptionsTest> arguments = CommandLineArguments<SingleSwitchOptionsTest>.Setup();
			SingleSwitchOptionsTest args = arguments.Parse(null);
			Assert.IsNotNull(args, "Expected an args object to be created.");
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseEmpty_InstanceCreated_DefaultNonNullableBoolean()
		{
			CommandLineArguments<SingleSwitchOptionsTest> arguments = CommandLineArguments<SingleSwitchOptionsTest>.Setup();
			SingleSwitchOptionsTest args = arguments.Parse(null);
			Assert.IsFalse(args.EnableLogging, "The enable logging option should be false by default.");
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseSwitch_LoggingTrue()
		{
			CommandLineArguments<SingleSwitchOptionsTest> arguments = CommandLineArguments<SingleSwitchOptionsTest>.Setup();
			SingleSwitchOptionsTest args = arguments.Parse("-EnableLog");
			Assert.IsTrue(args.EnableLogging, "The enable logging option should be true as switch is set.");
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseEmpty_InstanceCreated_TrueDefaultNonNullableBoolean()
		{
			CommandLineArguments<SingleSwitchOptionsTestDefaultTrue> arguments = CommandLineArguments<SingleSwitchOptionsTestDefaultTrue>.Setup();
			SingleSwitchOptionsTestDefaultTrue args = arguments.Parse(null);
			Assert.IsTrue(args.EnableLogging, "The enable logging option should be true by default.");
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseEmpty_InstanceCreated_EmptyPropertyCollection()
		{
			CommandLineArguments<ParameterArrayOptionsTestClass> arguments = CommandLineArguments<ParameterArrayOptionsTestClass>.Setup();
			ParameterArrayOptionsTestClass args = arguments.Parse(null);
			Assert.IsNotNull(args.Parameters);
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseEmpty_InstanceCreated_EmptyPropertyCollection_CheckCount()
		{
			CommandLineArguments<ParameterArrayOptionsTestClass> arguments = CommandLineArguments<ParameterArrayOptionsTestClass>.Setup();
			ParameterArrayOptionsTestClass args = arguments.Parse(null);
			Assert.AreEqual(0, args.Parameters.Count);
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseEmpty_InstanceCreated_PropertyCollection_CheckSingleParameter()
		{
			CommandLineArguments<ParameterArrayOptionsTestClass> arguments = CommandLineArguments<ParameterArrayOptionsTestClass>.Setup();
			ParameterArrayOptionsTestClass args = arguments.Parse(new[] { "-parameter:Name=UserA" });
			Assert.AreEqual(1, args.Parameters.Count);
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseAssemblyParameter_ExpectSuccess()
		{
			CommandLineArguments<SingleParameterOptions> arguments = CommandLineArguments<SingleParameterOptions>.Setup();
			SingleParameterOptions args = arguments.Parse("-assembly=Test.ServiceAssembly.Service");
			Assert.AreEqual("Test.ServiceAssembly.Service", args.Assembly, "Unexpected assembly value.");
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseAssemblyParameterWithQuotes_ExpectSuccess()
		{
			CommandLineArguments<SingleParameterOptions> arguments = CommandLineArguments<SingleParameterOptions>.Setup();
			SingleParameterOptions args = arguments.Parse("-assembly=\"Test.ServiceAssembly.Service\"");
			Assert.AreEqual("Test.ServiceAssembly.Service", args.Assembly, "Unexpected assembly value.");
		}

		[TestCase]
		public void ConstructCommandLineArguments_HasBannerResource()
		{
			CommandLineArguments<BannerTextResourceDefinedClass> arguments = CommandLineArguments<BannerTextResourceDefinedClass>.Setup();
			Assert.IsTrue(arguments.BannerDefined, "The banner has been defined in this class.");
		}

		[TestCase]
		public void ConstructCommandLineArguments_HasHelpResource()
		{
			CommandLineArguments<HelpTextResourceDefinedClass> arguments = CommandLineArguments<HelpTextResourceDefinedClass>.Setup();
			Assert.IsTrue(arguments.HelpResourceDefined, "The help resource has been defined in this class.");
		}

		[TestCase]
		public void ConstructCommandLineArguments_ParseAssemblyParameter_GetUsage()
		{
			var arguments = CommandLineArguments<SingleParameterOptions>.Setup();
			var usage = arguments.GetUsage();

			var expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine("Copyright (C) Redbridge Software 2020. All rights reserved.");
			expectedBuilder.AppendLine("Switches:");
			expectedBuilder.AppendLine();
			expectedBuilder.AppendLine("  -Assembly=(Assembly)");
			expectedBuilder.AppendLine("                      The assembly path");

			Assert.AreEqual(expectedBuilder.ToString(), usage);
		}

		[TestCase]
		public void ParseIntegerOptionalParameter_NoDefaultValue_NoArgument()
		{
			CommandLineArguments<IntegerParameterOptionsTest> arguments = CommandLineArguments<IntegerParameterOptionsTest>.Setup();
			IntegerParameterOptionsTest options = arguments.Parse(null);
			Assert.AreEqual(0, options.Port, "Unexpected port value.");
		}

		[TestCase]
		public void ParseIntegerOptionalParameter_NoDefaultValue_WithArgument()
		{
			CommandLineArguments<IntegerParameterOptionsTest> arguments = CommandLineArguments<IntegerParameterOptionsTest>.Setup();
			IntegerParameterOptionsTest options = arguments.Parse("-port=5050");
			Assert.AreEqual(5050, options.Port, "Unexpected port value.");
		}

		[TestCase]
		public void ParseIntegerOptionalParameter_WithDefaultValue_NoArguments()
		{
			CommandLineArguments<DefaultIntegerParameterOptionsTest> arguments = CommandLineArguments<DefaultIntegerParameterOptionsTest>.Setup();
			DefaultIntegerParameterOptionsTest options = arguments.Parse(null);
			Assert.AreEqual(1234, options.Port, "Unexpected port value.");
		}

		[TestCase]
		public void ParseIntegerOptionalParameter_WithDefaultValue_WithArgument()
		{
			CommandLineArguments<DefaultIntegerParameterOptionsTest> arguments = CommandLineArguments<DefaultIntegerParameterOptionsTest>.Setup();
			DefaultIntegerParameterOptionsTest options = arguments.Parse("-port=2020");
			Assert.AreEqual(2020, options.Port, "Unexpected port value.");
		}

		[TestCase]
		public void ParseEnumParameter_ValidEntry()
		{
			CommandLineArguments<EnumParameterOptions> arguments = CommandLineArguments<EnumParameterOptions>.Setup();
			EnumParameterOptions options = arguments.Parse("-Level=Verbose");
			Assert.AreEqual(SummaryOptions.Verbose, options.Level, "Unexpected level value.");
		}

		[TestCase]
		public void ParseEnumParameter_ValidEntry_LowerCase()
		{
			CommandLineArguments<EnumParameterOptions> arguments = CommandLineArguments<EnumParameterOptions>.Setup();
			EnumParameterOptions options = arguments.Parse("-Level=verbose");
			Assert.AreEqual(SummaryOptions.Verbose, options.Level, "Unexpected level value.");
		}

		[TestCase]
		public void GetDefaultEnumParameterValue_NullArguments()
		{
			CommandLineArguments<EnumParameterOptions> arguments = CommandLineArguments<EnumParameterOptions>.Setup();
			EnumParameterOptions options = arguments.Parse(null);
			Assert.AreEqual(SummaryOptions.Normal, options.Level, "Unexpected level value.");
		}
	}
}
