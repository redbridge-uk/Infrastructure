using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Redbridge.Console
{
    /// <summary>
    /// Represents the command line arguments set for a given type of TOptions
    /// </summary>
    /// <remarks>Use this arguments class to </remarks>
    public class CommandLineArguments<TOptions> 
        where TOptions:class, new()
    {
        private const char ParameterIndicator = '-';
        private const char ParameterValueIndicator = ':';
        private const char SetterIndicator = '=';
        private const char IndentationCharacter = ' ';
        private const int MaximumParameterNameWidth = 20;
        private const int SwitchIndentation = 2; 
        private readonly CommandLineOptionsCollection _arguments = new CommandLineOptionsCollection();
        private readonly CommandLineOptionsAttribute _globalOptions;

        #region Constructors

        /// <summary>
        /// Constructor for the command line arguments
        /// </summary>
        /// <param name="options"></param>
        /// <param name="arguments"></param>
        private CommandLineArguments(CommandLineOptionsAttribute options,
                                     IEnumerable<PropertyArgumentAttribute> arguments) 
        {
            _globalOptions = options;
            _arguments.AddRange(arguments);
        }

        #endregion Constructors

        /// <summary>
        /// Method that sets up the command line arguments set.
        /// </summary>
        /// <returns></returns>
        public static CommandLineArguments<TOptions> Setup()
        {
            Type optionsType = typeof(TOptions);
            CommandLineOptionsAttribute options = optionsType.GetCustomAttributes(typeof(CommandLineOptionsAttribute), true).Cast<CommandLineOptionsAttribute>().FirstOrDefault();

            // Walk all of the properties in the options file.
            PropertyInfo [] properties =  optionsType.GetProperties();

            List<PropertyArgumentAttribute> arguments = new List<PropertyArgumentAttribute>();
            foreach (PropertyInfo propertyInfo in properties)
            {
                PropertyArgumentAttribute attribute = propertyInfo.GetCustomAttributes(typeof(PropertyArgumentAttribute), true).Cast<PropertyArgumentAttribute>().SingleOrDefault();
                if ( attribute != null )
                {
                    attribute.Configure(propertyInfo);
                    arguments.Add(attribute);
                }
            }

            return new CommandLineArguments<TOptions>(options, arguments);
        }

        /// <summary>
        /// Method that parses the supplied arguments which typically are supplied from the command line.
        /// </summary>
        /// <returns>An instance of <typeparamref name="TOptions"/> with values populated according to the inputs
        /// supplied by the user.</returns>
        /// <param name="arguments">A set of parameters provided by the user on the command line.</param>
        /// <exception cref="OptionRequiredException"/>
        /// <example>
        ///     <code>MyApplicationOptions Parse("name=usera password=pwd")</code>
        /// </example>
        public TOptions Parse(params string [] arguments)
        {
            TOptions options = new TOptions();

            // Parse the arguments and apply the default values to the options.
            _arguments.SetDefaults(options);

            // If there are any command line arguments supplied, then process them into the options file...
            if (arguments != null && arguments.Any())
            {
                OnParse(options, arguments);
            }
            else
            {
                // No arguments have been supplied, so check that there aren't any defaults that have been skipped.
                PropertyArgumentAttribute attribute = _arguments.FirstOrDefault(required => required.Required);

                if (attribute != null)
                    throw new OptionRequiredException($"Switch/Parameter '{attribute.ParameterName}' is required.");
            }

            return options;
        }

        /// <summary>
        /// Method that parses the arguments and populates the options instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="arguments"></param>
        /// <exception cref="CommandLineParseException"/>
        /// <exception cref="UnknownOptionException"/>
        private void OnParse (TOptions options, params string[] arguments)
        {
            foreach (string argument in arguments)
            {
                string[] argumentParts = argument.Split(new char[] { SetterIndicator }, 2);
                string argumentName = argumentParts[0];
                string argumentValue = null;

                // If the argument has more than 1 part, then it also supplies a value.
                if (argumentParts.Length > 1)
                {
                    argumentValue = argumentParts[1];
                }

                if ( !argumentName.StartsWith(ParameterIndicator.ToString()) )
                {
                    throw new CommandLineParseException(
                        $"Please ensure that all parameters and switches are denoted with the delimiting character '{ParameterIndicator}'");
                }

                var trimmedArgumentName = argumentName.Trim(new char[] { ParameterIndicator });
                var splitNamedParameter = trimmedArgumentName.Split(new char[] { ParameterValueIndicator }).First();

                if (_arguments.TryGet(trimmedArgumentName, out var commandLineArg))
                {
                    commandLineArg.Parse(options, argumentValue);
                }
                else if (_arguments.TryGet(splitNamedParameter, out commandLineArg))
                {
                    // We know that the argument is for a split named parameter set.
                    var parameterName = trimmedArgumentName.Split(new char[] { ParameterValueIndicator }).Last();
                    commandLineArg.Parse(options, $"{parameterName}={argumentValue}"); 
                }
                else
                    throw new UnknownOptionException(
                        $"The supplied option '{argumentName}' is invalid or unknown, please check usage guide.");
            }
        }

        /// <summary>
        /// Gets whether a banner has been defined.
        /// </summary>
        public bool BannerDefined => _globalOptions != null && !string.IsNullOrWhiteSpace(_globalOptions.BannerResource);

        /// <summary>
        /// Gets whether a help resouces has been defined.
        /// </summary>
        public bool HelpResourceDefined => _globalOptions != null && !string.IsNullOrWhiteSpace(_globalOptions.HelpResource);

        /// <summary>
        /// Method that returns the usage text for the command line arguments set.
        /// </summary>
        public string GetUsage()
        {
            StringBuilder usageBuilder = new StringBuilder();

            // If a banner resource is defined.
            if (BannerDefined)
            {

            }
            else
                usageBuilder.Append(CreateDefaultBanner());

            // If a help text resource has been supplied, then show the syntax and description from it.
            if (HelpResourceDefined)
            {
                // Help resources are purely for displaying Syntax and Description information before the switches.
            }
            
            // We always generate the option usage from the class to avoid the two drifting out of sync.
            usageBuilder.Append(CreateOptionUsage());

            return usageBuilder.ToString();
        }

        /// <summary>
        /// Method that creates the option usage text.
        /// </summary>
        /// <returns></returns>
        private string CreateOptionUsage()
        {
            if (_arguments != null && _arguments.Count > 0)
            {
                StringBuilder optionUsageBuilder = new StringBuilder();
                optionUsageBuilder.AppendLine("Switches:");
                optionUsageBuilder.AppendLine();

                foreach (PropertyArgumentAttribute propertyArg in _arguments)
                {
                    string switchIndentation = new string(IndentationCharacter, SwitchIndentation);
                    string descriptionIndentation = new string(IndentationCharacter, SwitchIndentation + MaximumParameterNameWidth);
                    
                    string parameterDisplay = $"{switchIndentation}{ParameterIndicator}{propertyArg.ParameterDisplay}";
                    optionUsageBuilder.AppendLine(parameterDisplay);

                    string parameterDescription = $"{descriptionIndentation}{propertyArg.HelpText}";
                    optionUsageBuilder.AppendLine(parameterDescription);
                }

                return optionUsageBuilder.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Method that creates the default banner for a CLS application.
        /// </summary>
        /// <returns></returns>
        private string CreateDefaultBanner()
        {
            var bannerBuilder = new StringBuilder();
            var applicationAssembly = Assembly.GetEntryAssembly();

            if (applicationAssembly != null)
            {
                AssemblyName assemblyName = applicationAssembly.GetName();
                bannerBuilder.AppendFormat("CLS Services {0} Version {1}{2}", assemblyName.Name, assemblyName.Version, Environment.NewLine);
                bannerBuilder.AppendFormat("Copyright (C) CLS Services {0}. All rights reserved.{1}", DateTime.Now.Year, Environment.NewLine);
            }

            return bannerBuilder.ToString();
        }
    }
}
