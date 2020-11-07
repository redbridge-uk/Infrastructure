using System;
using System.Collections.Generic;
using System.Reflection;

namespace Redbridge.Console
{
    /// <summary>
    /// Represents a property argument attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class PropertyArgumentAttribute : ArgumentAttribute
    {
        /// <summary>
        /// Constructor for a switch attribute.
        /// </summary>
        /// <param name="name"></param>
        protected PropertyArgumentAttribute(string name) : base(name) { }

        /// <summary>
        /// Gets/sets the property that the argument relates.
        /// </summary>
        protected PropertyInfo Property
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets/sets the underlying property default value.
        /// </summary>
        protected object PropertyDefaultValue { get; set; }

        /// <summary>
        /// Method that configures the property argument to the attribute.
        /// </summary>
        /// <param name="propertyInfo"></param>
        internal void Configure(PropertyInfo propertyInfo)
        {
            Property = propertyInfo ?? throw new CommandLineParseException("The property info instance is not permitted to be null.");
            OnConfigure(propertyInfo);
        }
        
        protected virtual IEnumerable<Type> SupportedTypes => new Type[] {};

        internal virtual void OnConfigure(PropertyInfo propertyInfo) { }

        protected virtual void SetValue(object settings, object value)
        {
            Property.SetValue(settings, value, null);
        }

        public virtual void SetDefault(object settings)
        {
            SetValue(settings, PropertyDefaultValue);
        }

        public bool HasDefault => PropertyDefaultValue != null;
    }
}
