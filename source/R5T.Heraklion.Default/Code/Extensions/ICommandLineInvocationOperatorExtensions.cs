using System;
using System.IO;

using R5T.Caledonia;
using R5T.Heraklion.Extensions;


namespace R5T.Heraklion.Default
{
    public static class ICommandLineInvocationOperatorExtensions
    {
        public static OutputAndError ExecuteAndGetOutput(this ICommandLineInvocationOperator commandLineOperator, string executableFilePath, ICommandBuilderContext commandBuilderContext, TextWriter writer, bool suppresConsoleOutput = false)
        {
            var arguments = commandBuilderContext.BuildCommand();

            var invocation = CommandLineInvocation.New(executableFilePath, arguments, suppresConsoleOutput);

            var result = commandLineOperator.Run(invocation);

            if (result.ExitCode != 0)
            {
                throw new Exception($"Execution failed. Error:\n{result.GetErrorText()}\nOutput:\n{result.GetOutputText()}\nArguments:\n{arguments}");
            }

            var outputText = result.GetOutputText();
            var errorText = result.GetErrorText();

            var output = new OutputAndError()
            {
                Output = outputText,
                Error = errorText,
            };
            return output;
        }

        public static OutputAndError ExecuteAndGetOutput(this ICommandLineInvocationOperator commandLineOperator, string executableFilePath, ICommandBuilderContext commandBuilderContext, bool suppresConsoleOutput = false)
        {
            var output = commandLineOperator.ExecuteAndGetOutput(executableFilePath, commandBuilderContext, Console.Out, suppresConsoleOutput);
            return output;
        }

        public static void Execute(this ICommandLineInvocationOperator commandLineOperator, string executableFilePath, ICommandBuilderContext commandBuilderContext, TextWriter writer, bool suppresConsoleOutput = false)
        {
            var output = commandLineOperator.ExecuteAndGetOutput(executableFilePath, commandBuilderContext, writer, suppresConsoleOutput);

            writer.WriteLine(output.Output);
            writer.WriteLine(output.Error);
        }

        public static void Execute(this ICommandLineInvocationOperator commandLineOperator, string executableFilePath, ICommandBuilderContext commandBuilderContext, bool suppresConsoleOutput = false)
        {
            commandLineOperator.Execute(executableFilePath, commandBuilderContext, Console.Out, suppresConsoleOutput);
        }
    }
}
