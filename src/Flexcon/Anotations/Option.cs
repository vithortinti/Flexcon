namespace Flexcon.Anotations
{
    public class Option : Attribute
    {
        public string Value { get; }

        public Option(string value)
        {
            Value = value;
        }
    }
}
