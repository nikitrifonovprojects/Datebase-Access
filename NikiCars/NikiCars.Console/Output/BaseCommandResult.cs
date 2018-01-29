using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.Output
{
    public abstract class BaseCommandResult<T> : ICommandResult
    {
        private const string STATUS = "Status: ";
        private const string DATA = "Data: ";
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
            StringBuilder builder = new StringBuilder();
            builder.Append(STATUS);
            builder.AppendLine(GetStatus());

            if (this.item != null)
            {
                var json = JsonConvert.SerializeObject(this.item, Formatting.None, this.settings);
                builder.Append(DATA);
                builder.Append(json);
            }

            return builder.ToString();
        }
    }
}
