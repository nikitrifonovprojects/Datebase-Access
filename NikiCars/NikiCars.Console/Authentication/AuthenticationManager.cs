using System;
using System.Text;
using Newtonsoft.Json;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        public string CreateTokenString(ICommandUser commandUser)
        {
            Token token = new Token();
            token.Guid = Guid.NewGuid().ToString();
            token.Expiration = DateTime.Now.AddHours(1);
            token.UserData = commandUser.UserData;
            token.UserId = commandUser.ID;
            token.UserName = commandUser.Username;
            token.UserRoles = commandUser.UserRoles;

            string json = JsonConvert.SerializeObject(token);
            string result = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            return result;
        }

        public ICommandUser GetCommandUser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new CommandUser();
            }

            byte[] data = Convert.FromBase64String(token);
            string json = Encoding.UTF8.GetString(data);
            Token convertedToken = JsonConvert.DeserializeObject<Token>(json);

            ICommandUser user = new CommandUser();
            if (IsValid(convertedToken))
            {
                user.ID = convertedToken.UserId;
                user.IsAuthenticated = true;
                user.UserData = convertedToken.UserData;
                user.Username = convertedToken.UserName;
                user.UserRoles = convertedToken.UserRoles;
            }

            return user;
        }

        private bool IsValid(Token token)
        {
            if (DateTime.Now < token.Expiration)
            {
                return true;
            }

            return false;
        }
    }
}
