namespace Flexcon.Configuration
{
    internal static class FlexconConfiguration
    {
        /// <summary>
        /// Error message that should be displayed when the program throws an exception.
        /// You can use {Error} to indicate that this field should be replaced by the exception message.
        /// </summary>
        public static string ErrorMessage { get; set; }
        private static char? _parameterIdentifier;
        /// <summary>
        /// Used to identify parameters, if not set, will use '-' as default identifier
        /// </summary>
        public static char ParameterIdentifier { 
            get
            {
                if (_parameterIdentifier != null)
                    return (char)_parameterIdentifier;
                return '-';
            }
            set
            {
                _parameterIdentifier = value;
            }
        }
    }
}
