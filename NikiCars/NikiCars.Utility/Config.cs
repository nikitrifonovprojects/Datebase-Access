using Microsoft.Win32;
using NikiCars.Command.Interfaces;

namespace NikiCars.Utility
{
    public class Config : IConfig
    {
        private const string REGISTRY_PATH = @"Software\NikiProjects";
        private static string signingKey;
        private static string secutityKey;
        private static string connectionString;

        static Config()
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
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_PATH))
            {
                if (key != null)
                {
                    return key.GetValue("SigningKey").ToString();
                }
            }

            return null;
        }

        private static string GetSecutityKey()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_PATH))
            {
                if (key != null)
                {
                    return key.GetValue("SecutityKey").ToString();
                }
            }

            return null;
        }

        private static string GetConnectionString()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_PATH))
            {
                if (key != null)
                {
                    return key.GetValue("ConnectionString").ToString();
                }
            }

            return null;
        }
    }
}
