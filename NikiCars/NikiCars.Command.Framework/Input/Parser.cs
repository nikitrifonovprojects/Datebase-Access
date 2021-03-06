﻿using Newtonsoft.Json;
using NikiCars.Command.Interfaces;
using NikiCars.Common;

namespace NikiCars.Command.Framework.Input
{
    public class Parser : IParser
    {
        private IAuthenticationManager manager;
        private IModelBinder binder;

        public Parser(IAuthenticationManager manager, IModelBinder binder)
        {
            this.manager = manager;
            this.binder = binder;
        }

        public CommandContext ParseCommand(string input)
        {
            CommandContext context = new CommandContext();
            context.RawInput = input;

            if (!IsValidInput(input))
            {
                return context;
            }

            CommandRequest command = JsonConvert.DeserializeObject<CommandRequest>(input);

            Execute(command, context);

            return context;
        }

        private void Execute(CommandRequest command, CommandContext context)
        {
            ParseCommand(command, context);
            ParseData(command, context);
            ParseToken(command, context);
        }

        private void ParseToken(CommandRequest command, CommandContext context)
        {
            string value = string.Empty;
            if (command.Token != null)
            {
                value = command.Token;
            }

            context.CommandUser = this.manager.GetCommandUser(value);
        }

        private void ParseData(CommandRequest command, CommandContext context)
        {
            if (command.Data != null)
            {
                context.Properties = this.binder.BindModel(command.Data, command.Type);
            }
        }

        private void ParseCommand(CommandRequest command, CommandContext context)
        {
            if (!string.IsNullOrEmpty(command.Command))
            {
                context.CommandText = command.Command;
            }
        }

        private bool IsValidInput(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
    }
}
