using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Redbridge.Console
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    [DebuggerDisplay("Command Parameter: {ParameterDisplay}")]
    public class ParameterCollectionAttribute : ParameterAttribute
    {
        #region Constants

        private const char QuoteCharacter = '"';
        private const char EqualsCharacter = '=';
        private const string DefaultName = "Parameter";

        #endregion Constants

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ParameterCollectionAttribute() : base(DefaultName) { }

        /// <summary>
        /// Constructor for the string parameter attribute.
        /// </summary>
        /// <param name="name"></param>
        public ParameterCollectionAttribute(string name) : base(name) { }

        #endregion Constructors
        
        protected override void OnParse (object settings, string value)
        {
            if (value != null)
            {
                string []  nameValue = value.Split(EqualsCharacter);
                SetValue(settings, nameValue);
            }
            else
                SetValue(settings, null);
        }
        
        protected override void SetValue(object settings, object value)
        {
            var nameValue = (string []) value;
            var collection = (ParameterCollection) Property.GetValue(settings, null);
            collection.Add(nameValue.First(), nameValue.Last());
        }
        
        public string DefaultValue
        {
            get => (string) base.PropertyDefaultValue;
            set => PropertyDefaultValue = (string)value;
        }
        
        protected override IEnumerable<Type> SupportedTypes => new[] { typeof(ParameterCollection) };
    }
}
