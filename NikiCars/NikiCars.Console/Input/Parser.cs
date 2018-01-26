using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.Input
{
    public class Parser : IParser
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

        //public CommandContext ParseCommand(string input)
        //{
        //    CommandContext context = new CommandContext();
        //    context.Properties = new Dictionary<string, string>();
        //    context.RawInput = input;
        //    StringBuilder stringBuilder = new StringBuilder();

        //    int count = 0;
        //    while (count < input.Length)
        //    {
        //        if (input[count] == '{')
        //        {
        //            count++;
        //            while (input[count] != '}')
        //            {
        //                stringBuilder.Append(input[count]);
        //                count++;
        //            }
        //        }
        //        count++;
        //    }
        //    var b = stringBuilder.ToString();
        //    return context;
        //}
    }
}
