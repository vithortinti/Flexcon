# Flexcon
The Flexcon library is a tool for console applications that facilitates the creation of parameters. With it, developers can pass parameters to their applications more easily and intuitively. <br>
The library uses a self-referencing system to direct parameters to the desired object. This means that the developer doesn't have to worry about specifying the type or name of the object to which the parameter is being passed.

---

## Getting Started
This session contains the steps for using and starting the library.

1. The library started in version .NET Core 7.0. To use it, make sure you have this version installed.
2. Clone the repository in a directory of your choice.
```
git clone -b dev https://github.com/vithortinti/Flexcon.git
```
3. Open the project and you'll be ready to make changes.

---

## How to use
This session will explain how to use the Flexcon library.

### Main class
To execute the library, the following calls must be made in the Main method:
```cs
// Informs the Assembly of the executors.
Application app = new Application(Assembly.GetExecutingAssembly(), args);
// Performs some additional application settings.
app.Configure(
    parameterIdentifier: '-' // Character that will be used to identify the parameters in the console.
    );
// Runs to find and execute the executor specified in the console.
app.Run();
```

> NOTE: The identifier parameter can be positioned in the first position of the string that is passed as an argument to the 'Parameter' annotation without the risk of duplicating the identifier character.<br>
Examples: <br>
Configured as '-'<br>
[Parameter("-p")] will result in '-p'<br>
[Parameter("p")] will result in '-p'<br>
[Parameter("=p")] will result in '-=p'

### Executor class
The Executor class is the one that will be used to execute the methods based on the parameters.
```cs
using Flexcon.Anotations;
using Flexcon.Dependences;

namespace MyconsoleApplication
{
    // The option indicates which class should be instantiated, as you'll see in the console examples below.
    [Option("myexecutor")]
    public class MyExecutor : Executor
    {
        private string someContent = string.Empty;

        // The last method to be executed is the one that will perform the desired operation.
        public override void Execute(string[] args)
        {
            Console.WriteLine(someContent);
        }

        // The first parameter to be executed, as it is at the top of the C# file among the "Parameters" methods.
        // This is mandatory, if it is not specified in the console, the program will not execute the class.
        [Parameter("-x", Required = true)]
        public void MyXParameter(string value)
        {
            someContent = value;
        }

        // Second parameter to be executed.
        // It's not mandatory, so it won't matter if you don't specify it.
        [Parameter("-y")]
        public void MyYParameter(string value)
        {
            someContent += value;
        }
        
        // Third parameter to be executed.
        // It is only executed if the "-y" parameter is also specified in the console, if it is not specified in the console, the program will not execute the class.
        // It's not mandatory either, so it doesn't matter if it's not specified.
        [Parameter("-z", ReferenceTo = "-y")]
        public void MyZParameter(string value)
        {
            someContent += value;
        }
    }
}
```

When you execute the following command in the console:
```bash
dotnet run myexecutor -x Hello -z World -y " "
> Hello World
```
```bash
dotnet run myexecutor -x Hey -z Hello -y World 
> HeyWorldHello
```
The parameters don't need to be specified in a specific order or in the order they are in the classes, they just need to exist in the console.

---

## License
This library uses the MIT license, you can find out more about it [here](https://mit-license.org/).
