using System;
using System.Configuration;

namespace Redbridge.IntegrationTesting
{
	public class TestScenarioSection : ConfigurationSection
	{
		public static TestScenarioSection GetSectionOrDefault()
		{
			var section = ConfigurationManager.GetSection(SectionName);
			var scenarioSection = section as TestScenarioSection;
			return scenarioSection;
		}

		public static string SectionName => "TestScenario";

		[ConfigurationProperty("administratorUser", DefaultValue = "", IsRequired = false)]
		public string AdministratorUser
		{
			get { return (string)this["administratorUser"]; }
			set { this["administratorUser"] = value; }
		}

		[ConfigurationProperty("administratorPassword", DefaultValue = "", IsRequired = false)]
		public string AdministratorPassword
		{
			get { return (string)this["administratorPassword"]; }
			set { this["administratorPassword"] = value; }
		}
	}
}
