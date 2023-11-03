using Flexcon.Configuration;

namespace Flexcon.Anotations
{
    public class Parameter : Attribute
    {
        /// <summary>
        /// The parameter value.
        /// </summary>
        public string Value { get; set; }

        private string _referenceTo;
        /// <summary>
        /// Reference to a parameter, if the referenced paramenter do not exist, throw a error.
        /// </summary>
        public string ReferenceTo
        {
            get
            {
                return _referenceTo;
            }
            set
            {
                if (FlexconConfiguration.ParameterIdentifier == value[0])
                    _referenceTo = value;
                else
                    _referenceTo = FlexconConfiguration.ParameterIdentifier + value;
            }
        }

        /// <summary>
        /// Informs if the parameter is required for the execution.
        /// </summary>
        public bool Required { get; set; }

        public Parameter(string value)
        {
            if (FlexconConfiguration.ParameterIdentifier == value[0])
                Value = value;
            else
                Value = FlexconConfiguration.ParameterIdentifier + value;
        }

        public bool IsReferenced()
        {
            return ReferenceTo != null;
        }
    }
}
