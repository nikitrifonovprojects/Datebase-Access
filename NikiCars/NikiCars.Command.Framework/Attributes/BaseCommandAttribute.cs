﻿using System;
using NikiCars.Command.Interfaces;

namespace NikiCars.Command.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class BaseCommandAttribute : Attribute, ICommandActionExecutedFilter, ICommandActionExecutingFilter, ICommandResultExecutedFilter, ICommandResultExecutingFilter, ICommandExceptionFilter
    {
        public virtual void OnActionExecuted(CommandContext context)
        {

        }

        public virtual void OnActionExecuting(CommandContext context)
        {

        }

        public virtual void OnException(ExceptionContext context)
        {

        }

        public virtual void OnResultExecuted(CommandContext context)
        {

        }

        public virtual void OnResultExecuting(CommandContext context)
        {

        }
    }
}
