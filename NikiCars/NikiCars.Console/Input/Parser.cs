using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.Input
{
    public class Parser : IParser
    {
        public CommandContext ParseCommand(string input)
        {
            CommandContext context = new CommandContext();
            context.Properties = new Dictionary<string, string>();
            context.RawInput = input;

            if (!IsValidInput(input))
            {
                return context;
            }

            var parameters = input.Split(new char[] { ';' });
            for (int i = 0; i < parameters.Length; i++)
            {
                int index = parameters[i].IndexOf(':');
                string param = parameters[i].Substring(0, index);

                switch (param)
                {
                    case "command":
                        ParseCommand(parameters[i].Substring(index + 1), context);
                        break;
                    case "data":
                        ParseData(parameters[i].Substring(index + 1), context);
                        break;
                    case "token":
                        ParseToken(parameters[i].Substring(index + 1), context);
                        break;
                    default:
                        throw new ArgumentException("parameter not supported");
                }
            }

            return context;
        }

        private void ParseToken(string v, CommandContext context)
        {
            throw new NotImplementedException();
        }

        private void ParseData(string value, CommandContext context)
        {
            context.Properties = JsonConvert.DeserializeObject<Dictionary<string, string>>(value.Trim());
        }

        private void ParseCommand(string value, CommandContext context)
        {
            context.CommandText = value.Trim();
        }

        private bool IsValidInput(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
    }
}
