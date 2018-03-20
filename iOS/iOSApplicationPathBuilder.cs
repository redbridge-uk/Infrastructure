using System;
using Easilog.Client.SDK.DataAccess;

namespace Easilog.iOS
{
	public class iOSApplicationPathBuilder : IApplicationPathBuilder
	{
		public string BuildApplicationRootFolder()
		{
			return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Easilog";
		}

		public string BuildUserDataFolder(Guid userId)
		{
			var rootPath = BuildApplicationRootFolder();
			return $"{rootPath}\\{userId}\\Data";
		}

		public string BuildApplicationPath()
		{
			return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Easilog\\startup.json";
		}

		public string BuildDatabasePath(Guid userId)
		{
			return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Easilog\\{userId}\\Data\\easilog.sqlite";
		}

		public string BuildUserProfilePath(Guid userId)
		{
			return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Easilog\\{userId}\\Data\\userprofile.json";
		}

		public string BuildUserPreferencePath(Guid userId)
		{
			return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Easilog\\{userId}\\Data\\userpreferences.json";
		}
	}
}
