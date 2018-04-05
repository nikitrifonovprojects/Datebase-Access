using System;

namespace NikiCars.Logging
{
    public interface IExecutionTimerLogger : IDisposable
    {
        string CommandName { get; set; }
    }
}
