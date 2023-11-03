using Flexcon.Models;

namespace Flexcon.Dependences
{
    public abstract class Executor
    {
        private Arguments _arguments;

        public Executor(Arguments args)
        {
            _arguments = args;
        }

        public Executor()
        {

        }

        public abstract void Execute(string[] args);

        public virtual void Help() { }

        /// <summary>
        /// Try to get a index value. If doesn't exists, return a empty string.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Index value</returns>
        public string TryIndex(int index)
        {
            try
            {
                return _arguments.Args[index];
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Tries to collect the value of the inserted parameter. If doesn't exists, return a empty string.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Parameter value</returns>
        public string GetParameterValue(string parameter)
        {
            int parameterIndex = Array.IndexOf(_arguments.Args, parameter);
            int valueIndex = parameterIndex + 1;
            try
            {
                return _arguments.Args[valueIndex];
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool ParameterExists(string parameter)
        {
            int parameterIndex = Array.IndexOf(_arguments.Args, parameter);
            return parameterIndex != -1 ? true : false;
        }

        /// <summary>
        /// Shows an information message on the screen in yellow color
        /// </summary>
        /// <param name="message"></param>
        public void Information(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
