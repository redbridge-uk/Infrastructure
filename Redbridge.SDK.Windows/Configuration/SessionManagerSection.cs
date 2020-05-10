using System.Configuration;

namespace Redbridge.Configuration
{
	public class SessionManagerSection : ConfigurationSection
	{
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = false)]
		public string Name
		{
			get => (string)this["name"];
            set => this["name"] = value;
        }

		[ConfigurationProperty("user", DefaultValue = "", IsRequired = false)]
		public string Username
		{
			get => (string)this["user"];
            set => this["user"] = value;
        }

		[ConfigurationProperty("password", DefaultValue = "", IsRequired = false)]
		public string Password
		{
			get => (string)this["password"];
            set => this["password"] = value;
        }

		[ConfigurationProperty("redirect", DefaultValue = "", IsRequired = false)]
		public string Redirect
		{
			get => (string)this["redirect"];
            set => this["redirect"] = value;
        }

		[ConfigurationProperty("autoConnect", DefaultValue = "true", IsRequired = false)]
		public bool AutoConnect
		{
			get => (bool)this["autoConnect"];
            set => this["autoConnect"] = value;
        }

		[ConfigurationProperty("useHttps", DefaultValue = "true", IsRequired = false)]
		public bool UseHttps
		{
			get => (bool)this["useHttps"];
            set => this["useHttps"] = value;
        }

		[ConfigurationProperty("host", DefaultValue = "localhost", IsRequired = false)]
		public string Host
		{
			get => (string)this["host"];
            set => this["host"] = value;
        }

		[ConfigurationProperty("clientType", DefaultValue = "primary_web_app", IsRequired = false)]
		public string ClientType
		{
			get => (string)this["clientType"];
            set => this["clientType"] = value;
        }

		[ConfigurationProperty("cultureCode", DefaultValue = "en-GB", IsRequired = false)]
		public string CultureCode
		{
			get => (string)this["cultureCode"];
            set => this["cultureCode"] = value;
        }

		[ConfigurationProperty("port", DefaultValue = "3333", IsRequired = false)]
		public int Port
		{
			get => (int)this["port"];
            set => this["port"] = value;
        }
	}
}
