using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.Output
{
    public class ErrorResult<T> : ICommandResult
    {
        private T item;

        private string obj;
        private const string STATUS_ERROR = "Status: Error - ";

        public ErrorResult()
        {

        }

        public ErrorResult(string obj, T item)
        {
            this.item = item;
            this.obj = obj;
        }

        public string ExecuteResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            settings.Converters.Add(new StringEnumConverter());
            var json = JsonConvert.SerializeObject(this.item, Formatting.Indented, settings);

            StringBuilder builder = new StringBuilder();
            builder.Append(STATUS_ERROR);
            builder.AppendLine(this.obj);
            builder.Append(json);

            return builder.ToString();
        }
    }
}
