using System;
using System.Collections.Generic;
using System.Linq;

namespace NikiCars.Console.Input
{
    public class Parser
    {
        private char[] pattern = new char[] { '}', '{', ' ', '=', ',' };

        public CommandContext Parse(string input)
        {
            CommandContext context = new CommandContext();
            context.Properties = new Dictionary<string, string>();
            context.RawInput = input;
            var str = input.Split(this.pattern, StringSplitOptions.RemoveEmptyEntries).ToList();

            context.CommandText = str[0] + str[1];
            str.RemoveRange(0, 2);

            for (int i = 0; i < str.Count; i += 2)
            {
                context.Properties.Add(str[i], str[i + 1]);
            }

            return context;
        }

        public CommandContext ParseCommand(string input)
        {
            CommandContext context = new CommandContext();
            context.Properties = new Dictionary<string, string>();
            context.RawInput = input;


            return context;
        }
    }
}
