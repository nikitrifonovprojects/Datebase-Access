using System.Collections.Generic;
using Newtonsoft.Json;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.Input
{
    public class Parser : IParser
    {
        private IAuthenticationManager manager;

        public Parser(IAuthenticationManager manager)
        {
            this.manager = manager;
        }

        public CommandContext ParseCommand(string input)
        {
            CommandContext context = new CommandContext();
            context.Properties = new Dictionary<string, string>();
            context.RawInput = input;

            if (!IsValidInput(input))
            {
                return context;
            }

            Dictionary<string, string> inputParameters = new Dictionary<string, string>();

            var parameters = input.Split(new char[] { ';' });
            for (int i = 0; i < parameters.Length; i++)
            {
                int index = parameters[i].IndexOf(':');
                string param = parameters[i].Substring(0, index);
                string value = parameters[i].Substring(index + 1);

                inputParameters.Add(param, value);
            }

            Execute(inputParameters, context);

            return context;
        }

        private void Execute(Dictionary<string, string> inputParameters, CommandContext context)
        {
            ParseCommand(inputParameters, context);
            ParseData(inputParameters, context);
            ParseToken(inputParameters, context);
        }

        private void ParseToken(Dictionary<string, string> inputParameters, CommandContext context)
        {
            string value = string.Empty;
            if (inputParameters.ContainsKey("token"))
            {
                value = inputParameters["token"];
            }

            context.CommandUser = this.manager.GetCommandUser(value);
        }

        private void ParseData(Dictionary<string, string> inputParameters, CommandContext context)
        {
            if (inputParameters.ContainsKey("data"))
            {
                context.Properties = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputParameters["data"].Trim());
            }
        }

        private void ParseCommand(Dictionary<string, string> inputParameters, CommandContext context)
        {
            if (inputParameters.ContainsKey("command"))
            {
                context.CommandText = inputParameters["command"].Trim();
            }
        }

        private bool IsValidInput(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
    }
}
