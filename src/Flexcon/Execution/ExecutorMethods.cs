using Flexcon.Dependences;

namespace Flexcon.Execution
{
    internal class ExecutorMethods
    {
        private Executor _executor;
        private string[] _args;

        public ExecutorMethods(Executor executor, string[] args)
        {
            _executor = executor;
            _args = args;
        }

        /// <summary>
        /// Executes the final method "Execute".
        /// </summary>
        public void Execute()
        {
            var execute = _executor.GetType()
                .GetMethod("Execute");

            execute!.Invoke(_executor, new string[][] { _args });
        }
    }
}
