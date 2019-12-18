using System;
using System.IO;

using R5T.Caledonia;
using R5T.Heraklion.Extensions;


namespace R5T.Heraklion.Default
{
    public static class ICommandLineInvocationOperatorExtensions
    {
        public static void Execute(this ICommandLineInvocationOperator commandLineOperator, string executableFilePath, ICommandBuilderContext commandBuilderContext, TextWriter writer)
        {
            var arguments = commandBuilderContext.BuildCommand();

            var invocation = CommandLineInvocation.New(executableFilePath, arguments);

            var result = commandLineOperator.Run(invocation);

            if (result.ExitCode != 0)
            {
                throw new Exception($"Execution failed. Error:\n{result.GetErrorText()}\nOutput:\n{result.GetOutputText()}\nArguments:\n{arguments}");
            }
            else
            {
                writer.WriteLine(result.GetOutputText());
                writer.WriteLine(result.GetErrorText());
            }
        }

        public static void Execute(this ICommandLineInvocationOperator commandLineOperator, string executableFilePath, ICommandBuilderContext commandBuilderContext)
        {
            commandLineOperator.Execute(executableFilePath, commandBuilderContext, Console.Out);
        }
    }
}
