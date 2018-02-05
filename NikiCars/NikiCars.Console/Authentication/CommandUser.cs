using System.Collections.Generic;
using NikiCars.Command.Interfaces;

namespace NikiCars.Console.Authentication
{
    public class CommandUser : ICommandUser
    {
        public string ID { get; set; }

        public string Username { get; set; }

        public List<string> UserRoles { get; set; }

        public bool IsAuthenticated { get; set; }

        public object UserData { get; set; }

        public CommandUser()
        {
            this.UserRoles = new List<string>();
        }
    }
}
