using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class UserRepository : BaseRepository<User>
    {
        private const string PRIMARY_KEY = "UserID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(User item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Address", new SqlParameter() { Value = item.Address } },
                {"UserID", new SqlParameter() { Value = item.ID.ToString() } },
                {"Name", new SqlParameter() { Value = item.Name } },
                {"Email", new SqlParameter() { Value = item.Email } },
                {"LoginName", new SqlParameter() { Value = item.LoginName } },
                {"Password", new SqlParameter() { Value = item.Password } },
                {"MobilePhone", new SqlParameter() { Value = item.MobilePhone } },
                {"CityID", new SqlParameter() { Value = item.CityID.ToString() } },
                {"Type", new SqlParameter() { Value = item.Type } },
                {"Bulstat", new SqlParameter() { Value = item.Bulstat } },
                {"Website", new SqlParameter() { Value = item.Website } },
                {"PageName", new SqlParameter() { Value = item.PageName } },
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
            user.Type = Convert.ToString(reader["Type"]);
            user.Bulstat = Convert.ToString(reader["Bulstat"]);
            user.Website = Convert.ToString(reader["Website"]);
            user.PageName = Convert.ToString(reader["PageName"]);
            user.IsOfficialImporter = Convert.ToBoolean(reader["IsOfficialImporter"]);
            user.IsOrganisation = Convert.ToBoolean(reader["IsOrganisation"]);

            return user;
        }
    }
}
