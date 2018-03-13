using System;
using NikiCars.Command.Validation;

namespace NikiCars.Command.Interfaces
{
    public class ExceptionContext : CommandContext
    {
        private CommandContext context;

        public ExceptionContext(CommandContext context)
        {
            this.context = context;
        }

        public override string CommandText { get => this.context.CommandText; set => this.context.CommandText = value; }

        public override ICommandUser CommandUser { get => this.context.CommandUser; set => this.context.CommandUser = value; }

        public override EntityValidationResult ModelState { get => this.context.ModelState; set => this.context.ModelState = value; }

        public override dynamic Properties { get => this.context.Properties; set => this.context.Properties = value; }

        public override string RawInput { get => this.context.RawInput; set => this.context.RawInput = value; }

        public override ICommandResult ResponseResult { get => this.context.ResponseResult; set => this.context.ResponseResult = value; }

        public override string ResultString { get => this.context.ResultString; set => this.context.ResultString = value; }

        public bool IsHandled { get; set; }

        public Exception Exception { get; set; }
    }
}
