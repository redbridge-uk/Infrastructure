using System;
using System.Collections.Generic;
using System.Reflection;

namespace Redbridge.Console
{
    /// <summary>
    /// Switch attributes can only be used against bool or nullable boolean properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class SwitchAttribute : PropertyArgumentAttribute
    {
        /// <summary>
        /// Constructor for a switch attribute.
        /// </summary>
        /// <param name="name"></param>
        public SwitchAttribute(string name) : base(name) { }

        /// <summary>
        /// Method that configures the property info to the attribute.
        /// </summary>
        /// <param name="propertyInfo"></param>
        protected override void OnConfigure (PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType != typeof(bool) && propertyInfo.PropertyType != typeof(bool?))
                throw new CommandLineParseException(
                    $"The SwitchAttribute must only be used on boolean or nullable boolean types, property {propertyInfo.Name} has an invalid return type.");
        }

        /// <summary>
        /// Gets/sets the default value for the switch attribute.
        /// </summary>
        public bool DefaultValue
        {
            get { return (bool) PropertyDefaultValue; }
            set { PropertyDefaultValue = value; }
        }

        /// <summary>
        /// Gets the supported type of the parameter.
        /// </summary>
        protected override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(string) }; }
        }

        /// <summary>
        /// Method that parses the value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        protected override void OnParse (object settings, string value)
        {
            bool result;

            if ( !string.IsNullOrWhiteSpace(value) )
            {
                if (bool.TryParse(value, out result))
                {
                    SetValue(settings, result);
                }
                else
                    throw new CommandLineParseException(string.Format("Unable to convert '{0}' into a switch value.", value));
            }
            else
                SetValue(settings, true); // As its a switch, no value indicates that no additional criteria were provided by the user, this means 'true'.
        }
    }
}
