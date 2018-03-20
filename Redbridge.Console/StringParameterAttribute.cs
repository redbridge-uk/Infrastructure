using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Redbridge.Console
{
    /// <summary>
    /// Defines a string parameter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    [DebuggerDisplay("Command Parameter: {ParameterDisplay}")]
    public class StringParameterAttribute : ParameterAttribute
    {
        private const char QuoteCharacter = '"';

        /// <summary>
        /// Constructor for the string parameter attribute.
        /// </summary>
        /// <param name="name"></param>
        public StringParameterAttribute(string name) : base(name) { }

        /// <summary>
        /// Method that sets the parameter based on the supplied string value.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="value"></param>
        protected override void OnParse (object settings, string value)
        {
            if (value != null)
            {
                string trimmedValue = value.Trim(new char[] { QuoteCharacter });
                SetValue(settings, trimmedValue);
            }
            else
                SetValue(settings, null);
        }

        /// <summary>
        /// Gets/sets the default value.
        /// </summary>
        public string DefaultValue
        {
            get { return (string) base.PropertyDefaultValue; }
            set { PropertyDefaultValue = (string)value; }
        }

        /// <summary>
        /// Gets the supported type of the parameter.
        /// </summary>
        protected override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(string) }; }
        }
    }
}
