using Newtonsoft.Json;
using NikiCars.Command.Framework;
using NikiCars.Common;

namespace NikiCars.Command.Client
{
    public class CommandClient
    {
        private Host host;
        private string token;

        public CommandClient(Host host)
        {
            this.host = host;
        }

        public CommandResponce SendRequest<TIn>(string command, TIn item)
        {
            CommandRequest commandRequestData = new CommandRequest();
            commandRequestData.Command = command;
            commandRequestData.Data = JsonConvert.SerializeObject(item, typeof(TIn), null);
            commandRequestData.Type = typeof(TIn);
            commandRequestData.Token = this.token;

            string input = JsonConvert.SerializeObject(commandRequestData);

            string commandResult = this.host.ExecuteCommand(input);

            CommandResponce responce = JsonConvert.DeserializeObject<CommandResponce>(commandResult);

            if (responce.Status == "Authentication error")
            {
                ClearToken();
            }

            return responce;
        }

        public void SetToken(string token)
        {
            this.token = token;
        }

        public void ClearToken()
        {
            this.token = null;
        }
    }
}
