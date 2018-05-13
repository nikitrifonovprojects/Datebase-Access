using System;
using NikiCars.Command.Interfaces;
using NikiCars.Dependancy;
using NikiCars.Logging;

namespace NikiCars.Command.Framework
{
    public class Host : IHost
    {
        private IParser parser;
        private IDependencyContainer container;

        public Host(IParser parser, IDependencyContainer container)
        {
            this.parser = parser;
            this.container = container;
        }

        public string ExecuteCommand(string input)
        {
            CommandContext context = new CommandContext();
            try
            {
                context = this.parser.ParseCommand(input);
                
                if (context.CommandText == null)
                {
                    throw new ArgumentException(nameof(context.CommandText));
                }
                else
                {
                    if (this.container.IsRegistered(typeof(ICommand), context.CommandText))
                    {
                        using (var command = this.container.Resolve<ICommand>(context.CommandText, new DependencyObject(typeof(CommandContext), context)))
                        {
                            using (var executionLogger = this.container.Resolve<IExecutionTimerLogger>())
                            {
                                executionLogger.CommandName = context.CommandText;
                                return command.Execute();
                            }
                        }
                    }
                    else
                    {
                        using (var command = this.container.Resolve<NotFoundCommand>(new DependencyObject(typeof(CommandContext), context)))
                        {
                            return command.Execute();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (var command = this.container.Resolve<ServerErrorCommand>(new DependencyObject(typeof(CommandContext), context)))
                {
                    var executionLogger = this.container.Resolve<ILogger>();
                    executionLogger.LogError(ex.Message);

                    return command.Execute();
                }
            }
        }
    }
}
