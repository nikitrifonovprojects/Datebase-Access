using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.Output
{
    public abstract class BaseCommandResult<T> : ICommandResult
    {
        protected T item;

        public BaseCommandResult()
        {

        }

        public BaseCommandResult(T item)
        {
            this.item = item;
        }

        protected abstract string StatusMessage();

        public string ExecuteResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            settings.Converters.Add(new StringEnumConverter());
            var json = JsonConvert.SerializeObject(this.item, Formatting.None, settings);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(StatusMessage());
            builder.Append(json);

            return builder.ToString();
        }
    }
}
