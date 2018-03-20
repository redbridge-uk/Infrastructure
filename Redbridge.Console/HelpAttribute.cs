using System;

namespace Redbridge.Console
{
    /// <summary>
    /// Help Attribute for marking properties as returning help text.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class HelpAttribute : Attribute
    {
        /// <summary>
        /// Constructor for a help attribute, indicates that the property contains information for displaying the help information.
        /// </summary>
        public HelpAttribute() { }
    }
}
