using Flexcon.Configuration;
using Flexcon.Execution;
using Flexcon.Models;
using System.Reflection;
using System.Text;

namespace Flexcon
{
    public class Application
    {
#nullable disable
        private ExecutorWorker _executorWorker;
        private Arguments _arguments;

        public Application(Assembly assembly, string[] args)
        {
            // Validate inputs
            if (args.Length == 0)
                throw new ArgumentException("The program cannot start without arguments.");

            // Initialize and build the arguments
            _arguments = new Arguments()
            {
                Executor = args[0],
                Args = args.Skip(1).ToArray()
                // Ignore the first parameter (ToDo)
            };

            if (assembly == null)
                throw new NullReferenceException(nameof(assembly));

            // Create a ExecutorWorker, responsible for researching the executors
            _executorWorker = new ExecutorWorker(_arguments.Args, assembly);
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public void Run()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _executorWorker.GetExecutor(_arguments.Executor)
                .CallMethods(_arguments.Args)
                .Execute();
        }

        /// <summary>
        /// Configures the behavior of the console when it is run.
        /// </summary>
        /// <param name="parameterIdentifier">Parameter identification character in the console.</param>
        public void Configure(char parameterIdentifier)
        {
            FlexconConfiguration.ParameterIdentifier = parameterIdentifier;
        }
    }
}