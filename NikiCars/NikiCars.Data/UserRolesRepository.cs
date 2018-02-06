using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class UserRolesRepository : BaseRepository<UserRoles>
    {
        private const string PRIMARY_KEY = "RoleID";

        public UserRolesRepository(IConfig config) 
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(UserRoles item)
        {
            return new Dictionary<string, SqlParameter>
            {
                { "RoleName", new SqlParameter() { Value = item.RoleName } },
                { "RoleDescription", new SqlParameter() { Value = item.RoleDescription } }
            };
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetPrimaryKeyValue(UserRoles item)
        {
            return item.ID.ToString();
        }

        protected override string GetTableName()
        {
            return "UserRoles";
        }

        protected override void MapPrimaryKey(UserRoles item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override UserRoles MapProperties(SqlDataReader reader)
        {
            UserRoles userRoles = new UserRoles();
            userRoles.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            userRoles.RoleName = Convert.ToString(reader["RoleName"]);
            userRoles.RoleDescription = Convert.ToString(reader["RoleDescription"]);

            return userRoles;
        }
    }
}
