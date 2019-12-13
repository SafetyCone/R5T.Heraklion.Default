using System;

using R5T.Piraeus;


namespace R5T.Heraklion.Default
{
    public class CommandBuilderContext<TContext> : ICommandBuilderContext<TContext>
    {
        public ICommandBuilder CommandBuilder { get; }


        public CommandBuilderContext(ICommandBuilder commandBuilder)
        {
            this.CommandBuilder = commandBuilder;
        }

        public ICommandBuilderContext<TNewContext> ChangeContext<TNewContext>()
        {
            var newContextCommandBuilderContext = new CommandBuilderContext<TNewContext>(this.CommandBuilder);
            return newContextCommandBuilderContext;
        }
    }
}
