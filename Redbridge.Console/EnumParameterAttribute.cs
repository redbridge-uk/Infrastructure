using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Redbridge.Console
{
    /// <summary>
    /// Defines a string parameter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    [DebuggerDisplay("Enum Parameter: {ParameterDisplay}")]
    public class EnumParameterAttribute : ParameterAttribute        
    {
        /// <summary>
        /// Constructor for the string parameter attribute.
        /// </summary>
        /// <param name="name"></param>
        public EnumParameterAttribute(string name) : base(name) { }

        /// <summary>
        /// Method that sets the parameter based on the supplied string value.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="value"></param>
        protected override void OnParse (object settings, string value)
        {
            object enumValue = Enum.Parse(Property.PropertyType, value, true);
            SetValue(settings, enumValue);
        }

        /// <summary>
        /// Gets/sets the default value.
        /// </summary>
        public object DefaultValue
        {
            get { return base.PropertyDefaultValue; }
            set { PropertyDefaultValue = value; }
        }

        /// <summary>
        /// Gets the supported type of the parameter.
        /// </summary>
        protected override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(Enum) }; }
        }
    }
}
