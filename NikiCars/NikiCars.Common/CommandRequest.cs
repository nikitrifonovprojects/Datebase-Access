using System.Collections.Generic;

namespace NikiCars.Common
{
    public class CommandRequest
    {
        public string Command { get; set; }

        public Dictionary<string, string> Data { get; set; }

        public string Token { get; set; }
    }
}
