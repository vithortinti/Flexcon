namespace Flexcon.Validation
{
    public class RequiredParameterException : Exception
    {
        public RequiredParameterException(string message) : base(message) { }
        public RequiredParameterException() { }
    }
}
