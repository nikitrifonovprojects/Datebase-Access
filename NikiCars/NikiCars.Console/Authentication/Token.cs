using System;
using System.Collections.Generic;

namespace NikiCars.Console.Authentication
{
    public class Token
    {
        public string Guid { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<string> UserRoles { get; set; }

        public DateTime Expiration { get; set; }

        public object UserData { get; set; }
    }
}
