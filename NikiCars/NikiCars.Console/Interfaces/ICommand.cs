using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikiCars.Console.Interfaces
{
    public interface ICommand : IDisposable
    {
        string Execute();
    }
}
