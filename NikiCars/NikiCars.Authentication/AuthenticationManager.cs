using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NikiCars.Command.Interfaces;

namespace NikiCars.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private SymmetricSecurityKey signingKey;
        private SymmetricSecurityKey secutityKey;
        private string issuer = "self";

        public AuthenticationManager(IConfig config)
        {
            this.signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SigningKey));
            this.secutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecutityKey));
        }

        public string CreateTokenString(ICommandUser commandUser)
        {
            SigningCredentials singingCridentials = new SigningCredentials(this.signingKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
            EncryptingCredentials encryptingCridentials = new EncryptingCredentials(this.secutityKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>());
            claimsIdentity.AddClaim(new Claim(ClaimTypes.PrimarySid, commandUser.ID));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(commandUser.UserData)));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, commandUser.Username));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(commandUser.UserRoles)));

            SecurityTokenDescriptor tokenDiscriptor = new SecurityTokenDescriptor();
            tokenDiscriptor.Subject = claimsIdentity;
            tokenDiscriptor.Issuer = this.issuer;
            tokenDiscriptor.SigningCredentials = singingCridentials;
            tokenDiscriptor.EncryptingCredentials = encryptingCridentials;
            tokenDiscriptor.Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(1));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDiscriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public ICommandUser GetCommandUser(string tokenString)
        {
            ClaimsPrincipal token = ValidateToken(tokenString);

            if (token == null)
            {
                return new CommandUser();
            }

            string data = token.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;
            string id = token.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value;
            string name = token.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            string roles = token.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

            CommandUser user = new CommandUser();
            user.ID = id;
            user.IsAuthenticated = true;
            user.UserData = JsonConvert.DeserializeObject(data);
            user.Username = name;
            user.UserRoles = JsonConvert.DeserializeObject<List<string>>(roles);

            return user;
        }

        private ClaimsPrincipal ValidateToken(string tokenString)
        {
            if (string.IsNullOrEmpty(tokenString))
            {
                return null;
            }

            ClaimsPrincipal result;
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = new TimeSpan(0),
                ValidateAudience = false,
                IssuerSigningKey = this.signingKey,
                TokenDecryptionKey = this.secutityKey,
                ValidIssuer = this.issuer
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                result = tokenHandler.ValidateToken(tokenString, tokenValidationParameters, out SecurityToken securityToken);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
