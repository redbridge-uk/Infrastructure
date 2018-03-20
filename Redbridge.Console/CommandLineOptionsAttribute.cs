using System;

namespace Redbridge.Console
{
    /// <summary>
    /// Optional class for marking a class as an option set.
    /// </summary>
    /// <remarks>This is an optional attribute, but can provide additional information such as help file resources.</remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandLineOptionsAttribute : Attribute
    {
        /// <summary>
        /// Constructor for the command line options attribute.
        /// </summary>
        public CommandLineOptionsAttribute() { }

        /// <summary>
        /// Gets the name of the banner resource for displaying a banner if supplied.
        /// </summary>
        public string BannerResource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the help resource for displaying help in the event of a command line parsing issue.
        /// </summary>
        public string HelpResource
        {
            get;
            set;
        }
    }
}
