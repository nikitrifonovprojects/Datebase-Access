using System;
using System.Collections.Generic;
using NikiCars.Command.Interfaces;
using NikiCars.Dependancy;
using Unity.Resolution;

namespace NikiCars.Command.Framework
{
    public class Invoker
    {
        private IParser parser;
        private IDependencyContainer container;

        public Invoker(IParser parser, IDependencyContainer container)
        {
            this.parser = parser;
            this.container = container;
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
                    if (this.container.IsRegistered(typeof(ICommand), context.CommandText))
                    {
                        using (var command = this.container.Resolve<ICommand>(context.CommandText, new DependencyOverride(typeof(CommandContext), context)))
                        {
                            return command.Execute();
                        }
                    }
                    else
                    {
                        using (var command = this.container.Resolve<NotFoundCommand>(new DependencyOverride(typeof(CommandContext), context)))
                        {
                            return command.Execute();
                        }
                    }
                }
            }
            catch (Exception)
            {
                using (var command = this.container.Resolve<ServerErrorCommand>(new DependencyOverride(typeof(CommandContext), context)))
                {
                    return command.Execute();
                }
            }
        }
    }
}
