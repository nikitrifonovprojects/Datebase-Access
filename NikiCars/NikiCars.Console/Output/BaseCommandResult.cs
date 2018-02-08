using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NikiCars.Console.CommandClients;
using NikiCars.Command.Interfaces;

namespace NikiCars.Console.Output
{
    public abstract class BaseCommandResult<T> : ICommandResult
    {
        protected T item;
        private JsonSerializerSettings settings;

        public BaseCommandResult()
        {
            this.settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            this.settings.Converters.Add(new StringEnumConverter());
        }

        public BaseCommandResult(T item) 
            : this()
        {
            this.item = item;
        }

        protected abstract string GetStatus();

        public string ExecuteResult()
        {
            CommandResponce commandResponceData = new CommandResponce();
            commandResponceData.Status = GetStatus();
            commandResponceData.Data = this.item;

            return JsonConvert.SerializeObject(commandResponceData);
        }
    }
}
