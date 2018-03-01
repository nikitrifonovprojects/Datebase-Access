using System;
using NikiCars.Command.Framework.Output;
using NikiCars.Command.Interfaces;

namespace NikiCars.Command.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ValidateAttribute : BaseCommandAttribute
    {
        public override void OnActionExecuting(CommandContext context)
        {
            if (context.ModelState.HasError)
            {
                context.ResponseResult = new ErrorResult<string>(context.ModelState.ToString());
            }
        }
    }
}
