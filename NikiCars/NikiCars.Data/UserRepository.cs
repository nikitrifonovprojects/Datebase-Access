using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private const string PRIMARY_KEY = "UserID";

        public UserRepository(IConfig config) 
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(User item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Address", new SqlParameter() { Value = (object)item.Address ?? DBNull.Value } },
                {"Name", new SqlParameter() { Value = item.Name } },
                {"Email", new SqlParameter() { Value = item.Email } },
                {"LoginName", new SqlParameter() { Value = item.LoginName } },
                {"Password", new SqlParameter() { Value = item.Password } },
                {"MobilePhone", new SqlParameter() { Value = item.MobilePhone } },
                {"CityID", new SqlParameter() { Value = item.CityID.ToString() } },
                {"RoleID", new SqlParameter() { Value = item.RoleID.ToString() } },
                {"Type", new SqlParameter() { Value = (object)item.Type ?? DBNull.Value } },
                {"Bulstat", new SqlParameter() { Value = (object)item.Bulstat ?? DBNull.Value } },
                {"Website", new SqlParameter() { Value = (object)item.Website ?? DBNull.Value } },
                {"PageName", new SqlParameter() { Value = (object)item.PageName ?? DBNull.Value } },
                {"IsOrganisation", new SqlParameter() { Value = item.IsOrganisation.ToString() } },
                {"IsOfficialImporter", new SqlParameter() { Value = item.IsOfficialImporter.ToString() } }
            };
        }

        protected override string GetPrimaryKeyValue(User item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "Users";
        }

        protected override void MapPrimaryKey(User item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override User MapProperties(SqlDataReader reader)
        {
            User user = new User();
            user.ID = Convert.ToInt32(reader["UserID"]);
            user.Address = Convert.ToString(reader["Address"]);
            user.Name = Convert.ToString(reader["Name"]);
            user.Email = Convert.ToString(reader["Email"]);
            user.LoginName = Convert.ToString(reader["LoginName"]);
            user.Password = Convert.ToString(reader["Password"]);
            user.MobilePhone = Convert.ToString(reader["MobilePhone"]);
            user.CityID = Convert.ToInt32(reader["CityID"]);
            user.RoleID = Convert.ToInt32(reader["RoleID"]);
            user.Type = Convert.ToString(reader["Type"]);
            user.Bulstat = Convert.ToString(reader["Bulstat"]);
            user.Website = Convert.ToString(reader["Website"]);
            user.PageName = Convert.ToString(reader["PageName"]);
            user.IsOfficialImporter = Convert.ToBoolean(reader["IsOfficialImporter"]);
            user.IsOrganisation = Convert.ToBoolean(reader["IsOrganisation"]);

            return user;
        }

        public User GetUserByName(string loginName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE LoginName = @param1")
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            command.Parameters.AddWithValue("@param1", loginName);

            User user = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = MapProperties(reader);
                }
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Email = @param1")
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            command.Parameters.AddWithValue("@param1", email);

            User user = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = MapProperties(reader);
                }
            }

            return user;
        }

        public User GetUserByMobilePhone(string phone)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE MobilePhone = @param1")
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            command.Parameters.AddWithValue("@param1", phone);

            User user = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = MapProperties(reader);
                }
            }

            return user;
        }
    }
}
