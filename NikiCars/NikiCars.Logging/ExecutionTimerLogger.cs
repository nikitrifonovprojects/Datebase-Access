using System;
using System.Diagnostics;

namespace NikiCars.Logging
{
    public class ExecutionTimerLogger : IExecutionTimerLogger
    {
        private readonly ILogger log;
        private readonly Stopwatch stopwatch;

        public ExecutionTimerLogger(ILogger logger)
        {
            this.log = logger;
            this.stopwatch = Stopwatch.StartNew();
        }

        public string CommandName { get; set; }

        public void Dispose()
        {
            this.stopwatch.Stop();
            this.log.LogDebug(this.CommandName + " command took " + this.stopwatch.ElapsedMilliseconds + " ms to execute");
            GC.SuppressFinalize(this);
        }
    }
}
