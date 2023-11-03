using System.Globalization;

namespace Flexcon.Helpers
{
    public static class ArrayHelper
    {
        internal static Array ConvertArrayType(object[] array, Type type)
        {
            if (array == null) throw new NullReferenceException(nameof(array));

            var newArray = Array.CreateInstance(type, array.Length);
            var culture = CultureInfo.GetCultureInfo("en-US");

            for (int i = 0; i < array.Length; i++)
                newArray.SetValue(Convert.ChangeType(array[i], type, culture), i);

            return newArray;
        }

        internal static string[] ParameterValues(string param, char paramIdentifier, string[] args)
        {
            var actualParamIndex = Array.IndexOf(args, param);

            List<string> values = new();
            for (int i = actualParamIndex + 1; i < args.Length; i++)
            {
                if (args[i][0] == paramIdentifier) break;
                values.Add(args[i]);
            }

            return values.ToArray();
        }
    }
}
