using System;

namespace NikiCars.Common
{
    public class CommandRequest
    {
        public string Command { get; set; }

        public string Data { get; set; }

        public Type Type { get; set; }

        public string Token { get; set; }
    }
}
