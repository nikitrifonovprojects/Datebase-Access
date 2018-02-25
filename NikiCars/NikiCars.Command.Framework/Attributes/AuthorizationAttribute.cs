using System;
using NikiCars.Command.Framework.Output;
using NikiCars.Command.Interfaces;

namespace NikiCars.Command.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class,
                       AllowMultiple = true)
    ]
    public class AuthorizationAttribute : Attribute, ICommandAuthorizationFilter
    {
        private string[] roles;

        public AuthorizationAttribute()
            : this(null)
        {

        }

        public AuthorizationAttribute(params string[] roles)
        {
            this.roles = roles;
        }

        public void OnAuthorize(CommandContext context)
        {
            if (!context.CommandUser.IsAuthenticated)
            {
                context.ResponseResult = new AuthorizationErrorResult<string>();
            }

            if (this.roles != null)
            {
                foreach (var role in this.roles)
                {
                    if (context.CommandUser.UserRoles.Contains(role))
                    {
                        return;
                    }
                }
            }
            else
            {
                context.ResponseResult = new AuthorizationErrorResult<string>();
            }
        }
    }
}
