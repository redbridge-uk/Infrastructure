using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Redbridge.Console
{
    /// <summary>
    /// Defines a integer parameter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    [DebuggerDisplay("Int32 Command Parameter: {ParameterDisplay}")]
    public class IntegerParameterAttribute : ParameterAttribute
    {
        /// <summary>
        /// Constructor for the string parameter attribute.
        /// </summary>
        /// <param name="name"></param>
        public IntegerParameterAttribute(string name) : base(name) { }

        /// <summary>
        /// Method that sets the parameter based on the supplied string value.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="value"></param>
        protected override void OnParse (object settings, string value)
        {
            int intValue;
            if (int.TryParse(value, out intValue))
            {
                SetValue(settings, intValue);
            }
            else
                throw new CommandLineParseException(string.Format("The supplied value {0} for parameter {1} is invalid. Please provide an integer value.", value, ParameterName));
        }

        /// <summary>
        /// Gets the supported type of the parameter.
        /// </summary>
        protected override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(int) }; }
        }

        /// <summary>
        /// Gets/sets the default value.
        /// </summary>
        public int DefaultValue
        {
            get { return (int)base.PropertyDefaultValue; }
            set { PropertyDefaultValue = (int)value; }
        }
    }
}
