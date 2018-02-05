using System;

namespace NikiCars.Command.Interfaces
{
    public interface ICommand : IDisposable
    {
        string Execute();
    }
}
