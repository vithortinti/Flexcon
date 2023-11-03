using Flexcon.Anotations;
using Flexcon.Configuration;
using Flexcon.Dependences;
using Flexcon.Helpers;
using Flexcon.Validation;
using System.Reflection;

namespace Flexcon.Execution
{
    internal class ParametersMethods
    {
        private Executor _executor;
        private string[] _args;

        internal ParametersMethods(Executor executor, string[] args)
        {
            _executor = executor;
            _args = args;
        }

        /// <summary>
        /// Calls all the parameter methods that match what was passed to the console.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Execution instance of the final "Execute" method</returns>
        /// <exception cref="ReferecedParameterException"></exception>
        /// <exception cref="RequiredParameterException"></exception>
        internal ExecutorMethods CallMethods(string[] args)
        {
            var executorType = _executor.GetType();
            var methods = executorType.GetMethods()
                .Where(x => x.IsDefined(typeof(Parameter)));

            foreach (var method in methods)
            {
                var param = (Parameter)method.GetCustomAttribute(typeof(Parameter))!;
                var index = Array.IndexOf(args, param.Value);

                if (index >= 0)
                {
                    if (param.IsReferenced())
                    {
                        bool exists = ReferencedExists(param, args);
                        if (!exists) throw new ReferecedParameterException($"The parameter {param.Value} is only used when the {param.ReferenceTo} is also used.");
                    }

                    object[] values;
                    SetMethodParameters(param.Value, method, out values);

                    method.Invoke(_executor, values);
                }
                else if (param.Required)
                {
                    throw new RequiredParameterException($"The parameter {param.Value} is required.");
                }
            }

            return new ExecutorMethods(_executor, _args);
        }

        private void SetMethodParameters(string param, MethodInfo method, out object[] values)
        {
            var parameters = method.GetParameters();
            values = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var paramType = parameters[i].ParameterType;

                // Check if the parameter is an array
                // For a array to be executed correctly, it must be the last parameter specified in the method.
                if (paramType.IsArray)
                {
                    bool isTheLastPosition = i - (parameters.Length - 1) == 0;
                    if (isTheLastPosition)
                    {
                        string[] arrValues = ArrayHelper.ParameterValues(param, FlexconConfiguration.ParameterIdentifier, _args);
                        var finalArr = ArrayHelper.ConvertArrayType(arrValues, paramType.GetElementType()!);

                        values[i] = finalArr;
                    }
                    else
                    {
                        throw new InvalidOperationException("A method parameter that is an array must be the last parameter of the method.");
                    }
                }
                else
                {
                    int paramIndex = Array.IndexOf(_args, param) + i + 1;
                    values[i] = Convert.ChangeType(_args[paramIndex], paramType);
                }
            }
        }

        private bool ReferencedExists(Parameter param, string[] args)
        {
            return Array.IndexOf(args, param.ReferenceTo) != -1 ? true : false;
        }
    }
}
