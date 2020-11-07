using System;

namespace Redbridge.Console
{
    [Serializable]
    public class OptionRequiredException : CommandLineParseException
    {
        /// <summary>
        /// 
        /// </summary>
        public OptionRequiredException() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public OptionRequiredException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public OptionRequiredException(string message, Exception inner) : base(message, inner) { }
    }
}
