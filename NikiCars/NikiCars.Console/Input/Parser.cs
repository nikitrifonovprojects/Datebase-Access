using System.Collections.Generic;
using System.Text;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.Input
{
    public class Parser : IParser
    {
        public CommandContext ParseCommand(string input)
        {
            CommandContext context = new CommandContext();
            context.RawInput = input;
            StringBuilder stringBuilder = new StringBuilder();

            bool commandIsAdded = false;
            bool itemTypeIsAdded = false;
            bool propNameIsAdded = false;
            bool propValueIsAdded = false;

            int count = 0;
            List<string> result = new List<string>();
            while (count < input.Length)
            {
                if (input[count] == '^')
                {
                    count++;

                    if (count <= input.Length - 2)
                    {
                        stringBuilder.Append(input[count]);
                        count++;
                    }
                }

                if (input[count] == ',' || input[count] == '=' || count == input.Length - 1)
                {
                    if (count == input.Length - 1)
                    {
                        stringBuilder.Append(input[count]);
                    }
                    if (!commandIsAdded)
                    {
                        stringBuilder.Append(' ');
                        context.CommandText = stringBuilder.ToString();
                        stringBuilder.Clear();
                        commandIsAdded = true;
                    }
                    else if (!itemTypeIsAdded)
                    {
                        context.CommandText += stringBuilder.ToString();
                        stringBuilder.Clear();
                        itemTypeIsAdded = true;
                    }
                    else if (!propNameIsAdded)
                    {
                        result.Add(stringBuilder.ToString());
                        stringBuilder.Clear();
                        propNameIsAdded = true;
                    }
                    else if (!propValueIsAdded)
                    {
                        result.Add(stringBuilder.ToString());
                        stringBuilder.Clear();
                        propNameIsAdded = false;
                    }
                }
                else
                {
                    stringBuilder.Append(input[count]);
                }

                count++;
            }

            if (result.Count > 1)
            {
                context.Properties = new Dictionary<string, string>();

                for (int i = 0; i < result.Count; i+=2)
                {
                    context.Properties.Add(result[i], result[i + 1]);
                }
            }

            return context;
        }
    }
}
