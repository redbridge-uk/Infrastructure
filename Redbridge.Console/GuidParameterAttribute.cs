using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Redbridge.Console
{
    /// <summary>
    /// Defines a Guid parameter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    [DebuggerDisplay("Guid Command Parameter: {ParameterDisplay}")]
    public class GuidParameterAttribute : ParameterAttribute
    {
        /// <summary>
        /// Constructor for the string parameter attribute.
        /// </summary>
        /// <param name="name"></param>
        public GuidParameterAttribute(string name) : base(name) { }

        /// <summary>
        /// Method that sets the parameter based on the supplied string value.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="value"></param>
        protected override void OnParse (object settings, string value)
        {
            Guid guidValue;
            if (Guid.TryParse(value, out guidValue))
            {
                SetValue(settings, guidValue);
            }
            else
                throw new CommandLineParseException(string.Format("The supplied value {0} for parameter {1} is invalid. Please provide a Guid value.", value, ParameterName));
        }

        /// <summary>
        /// Gets the supported type of the parameter.
        /// </summary>
        protected override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(Guid) }; }
        }

        /// <summary>
        /// Gets/sets the default value.
        /// </summary>
        public Guid DefaultValue
        {
            get { return (Guid)base.PropertyDefaultValue; }
            set { PropertyDefaultValue = (Guid)value; }
        }
    }
}
