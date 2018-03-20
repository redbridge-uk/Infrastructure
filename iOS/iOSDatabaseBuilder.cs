using System;
using Easilog.Client.SDK.DataAccess;
using Easilog.SDK.Diagnostics;
using Easilog.SDK.IO;

namespace Easilog.iOS
{
	public class iOSDatabaseBuilder : DatabaseBuilder
	{
		public iOSDatabaseBuilder(IApplicationPathBuilder pathBuilder, IFileManipulator fileManipulator, ILogger logger) : base(pathBuilder, fileManipulator, logger)
        {
		}
	}
}
