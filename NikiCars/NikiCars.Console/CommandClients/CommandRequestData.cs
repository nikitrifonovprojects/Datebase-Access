﻿using System.Collections.Generic;

namespace NikiCars.Console.CommandClients
{
    public class CommandRequestData
    {
        public string Command { get; set; }

        public Dictionary<string, string> Data { get; set; }

        public string Token { get; set; }
    }
}