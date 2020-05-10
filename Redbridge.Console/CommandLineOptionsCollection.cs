
namespace Redbridge.Console
{
    /// <summary>
    /// Represents a collection of command line options.
    /// </summary>
    public class CommandLineOptionsCollection : MultiKeyedCollection<string, string, PropertyArgumentAttribute>
    {
        /// <summary>
        /// Gets the full name for the argument attribute.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override string GetKey1ForItem (PropertyArgumentAttribute item)
        {
            return PreProcessKey1(item.ParameterName);
        }

        /// <summary>
        /// Gets the short hand name for the argument attribute.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override string GetKey2ForItem(PropertyArgumentAttribute item)
        {
            return PreProcessKey2(item.ShortHandName ?? item.ParameterName);
        }

        /// <summary>
        /// Method that pre-processes the supplied key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override string PreProcessKey1(string key)
        {
            return key.ToLower();
        }

        /// <summary>
        /// Method that pre-processes the supplied key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override string PreProcessKey2(string key)
        {
            return key.ToLower();
        }

        /// <summary>
        /// Method that sets all defaults on the options object.
        /// </summary>
        /// <param name="options"></param>
        internal void SetDefaults(object options)
        {
            foreach (var propertyAttribute in this)
            {
                if (propertyAttribute.HasDefault)
                    propertyAttribute.SetDefault(options);
            }
        }
    }
}
