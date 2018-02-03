using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using NikiCars.Console.Commands;

namespace NikiCars.Console.CommandClients
{
    public class CommandClient
    {
        private Invoker invoker;
        private string token;

        public CommandClient(Invoker invoker)
        {
            this.invoker = invoker;
        }

        public CommandResponceData SendRequest<TIn>(string command, TIn item)
        {
            CommandRequestData commandRequestData = new CommandRequestData();
            commandRequestData.Command = command;
            commandRequestData.Data = CreateProperties(item);
            commandRequestData.Token = this.token;

            string input = JsonConvert.SerializeObject(commandRequestData);

            string commandResult = this.invoker.ExecuteCommand(input);

            CommandResponceData responce = JsonConvert.DeserializeObject<CommandResponceData>(commandResult);

            if (command == "login User" && responce.Status == "Success")
            {
                SetToken(Convert.ToString(responce.Data));
                responce.Data = null;
            }

            if (responce.Status == "Authentication error")
            {
                ClearToken();
            }

            return responce;
        }

        private void SetToken(string token)
        {
            this.token = token;
        }

        private void ClearToken()
        {
            this.token = null;
        }

        private Dictionary<string, string> CreateProperties<TIn>(TIn item)
        {
            return item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.GetValue(item) != null)
                    .ToDictionary(prop => prop.Name, prop => Convert.ToString(prop.GetValue(item)));
        }
    }
}
