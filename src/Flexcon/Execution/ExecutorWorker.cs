using Flexcon.Anotations;
using Flexcon.Configuration;
using Flexcon.Dependences;
using System.Reflection;

namespace Flexcon.Execution
{
    internal class ExecutorWorker
    {
        private readonly Assembly _assembly;
        private string[] _args;

        public ExecutorWorker(string[] args, Assembly assembly)
        {
            _assembly = assembly;
            _args = args;
        }

        /// <summary>
        /// Responsible for instantiating an Executor class
        /// </summary>
        /// <param name="executor"></param>
        /// <returns>All parameter methods</returns>
        /// <exception cref="Exception"></exception>
        public ParametersMethods GetExecutor(string executor)
        {
            var classe = _assembly.GetTypes()
                .Where(x => x.IsDefined(typeof(Option)) && x.BaseType == typeof(Executor))
                .FirstOrDefault(x => ((Option)x.GetCustomAttribute(typeof(Option))!).Value == executor)
                    ?? throw new Exception($"There is no option for {executor}.");

            var executorInstance = (Executor)Activator.CreateInstance(classe)!;

            return new ParametersMethods(executorInstance, _args);
        }
    }
}
