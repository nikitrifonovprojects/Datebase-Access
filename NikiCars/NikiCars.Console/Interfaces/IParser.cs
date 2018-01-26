using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikiCars.Console.Input;

namespace NikiCars.Console.Interfaces
{
    public interface IParser
    {
        CommandContext Parse(string input);
    }
}
