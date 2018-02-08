using System.Configuration;
using NikiCars.Command.Interfaces;

namespace NikiCars.Console.Utility
{
    public class DeveloperConfig : IConfig
    {
        private static string signingKey;
        private static string secutityKey;
        private static string connectionString;

        static DeveloperConfig()
        {
            signingKey = GetSigningKey();
            secutityKey = GetSecutityKey();
            connectionString = GetConnectionString();
        }

        public string SigningKey
        {
            get
            {
                return signingKey;
            }
        }

        public string SecutityKey
        {
            get
            {
                return secutityKey;
            }
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        private static string GetSigningKey()
        {
            return ConfigurationManager.AppSettings["signingKey"];
        }

        private static string GetSecutityKey()
        {
            return ConfigurationManager.AppSettings["secutityKey"];
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}
