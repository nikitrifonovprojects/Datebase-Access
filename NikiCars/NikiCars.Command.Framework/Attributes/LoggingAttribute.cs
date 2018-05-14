using System;
using NikiCars.Command.Interfaces;
using NikiCars.Logging;

namespace NikiCars.Command.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LoggingAttribute : BaseCommandAttribute
    {
        private ILogger logger;

        public LoggingAttribute(ILogger logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            this.logger.LogError(context.Exception.Message);
        }
    }
}
