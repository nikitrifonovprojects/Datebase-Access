using System.Collections.Generic;

namespace NikiCars.Command.Interfaces
{
    public interface ICommandUser
    {
        string ID { get; set; }

        string Username { get; set; }

        List<string> UserRoles { get; set; }

        bool IsAuthenticated { get; set; }

        object UserData { get; set; }

    }
}
