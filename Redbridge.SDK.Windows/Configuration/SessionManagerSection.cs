using System;
using System.Configuration;

namespace Redbridge.Windows.Configuration
{
	public class SessionManagerSection : ConfigurationSection
	{
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = false)]
		public string Name
		{
			get
			{
				return (string)this["name"];
			}
			set
			{
				this["name"] = value;
			}
		}

		[ConfigurationProperty("user", DefaultValue = "", IsRequired = false)]
		public string Username
		{
			get
			{
				return (string)this["user"];
			}
			set
			{
				this["user"] = value;
			}
		}

		[ConfigurationProperty("password", DefaultValue = "", IsRequired = false)]
		public string Password
		{
			get
			{
				return (string)this["password"];
			}
			set
			{
				this["password"] = value;
			}
		}

		[ConfigurationProperty("redirect", DefaultValue = "", IsRequired = false)]
		public string Redirect
		{
			get
			{
				return (string)this["redirect"];
			}
			set
			{
				this["redirect"] = value;
			}
		}

		[ConfigurationProperty("autoConnect", DefaultValue = "true", IsRequired = false)]
		public Boolean AutoConnect
		{
			get
			{
				return (Boolean)this["autoConnect"];
			}
			set
			{
				this["autoConnect"] = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("useHttps", DefaultValue = "true", IsRequired = false)]
		public Boolean UseHttps
		{
			get
			{
				return (Boolean)this["useHttps"];
			}
			set
			{
				this["useHttps"] = value;
			}
		}

		[ConfigurationProperty("host", DefaultValue = "localhost", IsRequired = false)]
		public string Host
		{
			get
			{
				return (string)this["host"];
			}
			set
			{
				this["host"] = value;
			}
		}

		[ConfigurationProperty("clientType", DefaultValue = "primary_web_app", IsRequired = false)]
		public string ClientType
		{
			get
			{
				return (string)this["clientType"];
			}
			set
			{
				this["clientType"] = value;
			}
		}

		[ConfigurationProperty("cultureCode", DefaultValue = "en-GB", IsRequired = false)]
		public string CultureCode
		{
			get
			{
				return (string)this["cultureCode"];
			}
			set
			{
				this["cultureCode"] = value;
			}
		}

		[ConfigurationProperty("port", DefaultValue = "3333", IsRequired = false)]
		public int Port
		{
			get
			{
				return (int)this["port"];
			}
			set
			{
				this["port"] = value;
			}
		}
	}
}
