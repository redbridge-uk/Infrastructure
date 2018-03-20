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
        private object m_defaultValue;

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
        protected object PropertyDefaultValue
        {
            get { return m_defaultValue; }
            set { m_defaultValue = value; }
        }

        /// <summary>
        /// Method that configures the property argument to the attribute.
        /// </summary>
        /// <param name="propertyInfo"></param>
        internal void Configure(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new CommandLineParseException("The property info instance is not permitted to be null.");

            Property = propertyInfo;
            OnConfigure(propertyInfo);
        }
        
        protected virtual IEnumerable<Type> SupportedTypes => new Type[] {};
        protected virtual void OnConfigure(PropertyInfo propertyInfo) { }

        protected virtual void SetValue(object settings, object value)
        {
            Property.SetValue(settings, value, null);
        }

        public virtual void SetDefault(object settings)
        {
            SetValue(settings, PropertyDefaultValue);
        }

        public bool HasDefault => m_defaultValue != null;
    }
}
