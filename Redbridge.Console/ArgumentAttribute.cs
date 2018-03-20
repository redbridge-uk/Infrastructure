using System;

namespace Redbridge.Console
{
    public abstract class ArgumentAttribute : Attribute
    {
        /// <summary>
        /// Represents an argument attribute.
        /// </summary>
        /// <param name="parameterName"></param>
        public ArgumentAttribute (string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new CommandLineParseException("The parameter name is not permitted to be null or whitespace");

            ParameterName = parameterName;
            AllowEmptyValues = false;
        }

        #region Public Properties

        /// <summary>
        /// Get/sets whether the argument supports empty values.
        /// </summary>
        /// <remarks>Default value is false.</remarks>
        public bool AllowEmptyValues
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets whether the parameter name has a short-hand representation e.g. 'i' instead of 'install' 
        /// </summary>
        public string ShortHandName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the arguments is required.
        /// </summary>
        public bool Required
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the help text for the argument attribute.
        /// </summary>
        public string HelpText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string ParameterName
        {
            get;
            private set;
        }

        #endregion Public Properties

        /// <summary>
        /// Method that parses the supplied value.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="value"></param>
        public void Parse (object settings, string value)
        {
            OnParse(settings, value);
        }

        /// <summary>
        /// Method that processes the parsing of the value.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="value"></param>
        protected abstract void OnParse(object settings, string value);

        /// <summary>
        /// Gets the parameter display text.
        /// </summary>
        public virtual string ParameterDisplay 
        {
            get { return ParameterName; }
        }
    }
}
