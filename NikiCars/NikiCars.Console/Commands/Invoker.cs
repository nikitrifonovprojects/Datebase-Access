using System;
using System.Collections.Generic;
using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using Unity.Resolution;

namespace NikiCars.Console.Commands
{
    public class Invoker
    {
        private IParser parser;

        public Invoker(IParser parser)
        {
            this.parser = parser;
        }

        public string ExecuteCommand(string input)
        {
            CommandContext context = new CommandContext();
            context.Properties = new Dictionary<string, string>();
            try
            {
                context = this.parser.ParseCommand(input);
                
                if (context.CommandText == null)
                {
                    throw new ArgumentException(nameof(context.CommandText));
                }
                else
                {
                    if (DependencyContainer.IsRegistered(typeof(ICommand), context.CommandText))
                    {
                        using (var command = DependencyContainer.Resolve<ICommand>(context.CommandText, new DependencyOverride(typeof(CommandContext), context)))
                        {
                            return command.Execute();
                        }
                    }
                    else
                    {
                        using (var command = DependencyContainer.Resolve<NotFoundCommand>(new DependencyOverride(typeof(CommandContext), context)))
                        {
                            return command.Execute();
                        }
                    }
                }
            }
            catch (Exception)
            {
                using (var command = DependencyContainer.Resolve<ServerErrorCommand>(new DependencyOverride(typeof(CommandContext), context)))
                {
                    return command.Execute();
                }
            }
        }
    }
}
