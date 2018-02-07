using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class UserRoleRepository : BaseRepository<UserRole>
    {
        private const string PRIMARY_KEY = "RoleID";

        public UserRoleRepository(IConfig config) 
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(UserRole item)
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

        protected override string GetPrimaryKeyValue(UserRole item)
        {
            return item.ID.ToString();
        }

        protected override string GetTableName()
        {
            return "UserRoles";
        }

        protected override void MapPrimaryKey(UserRole item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override UserRole MapProperties(SqlDataReader reader)
        {
            return StaticMapProperties(reader);
        }

        internal static UserRole StaticMapProperties(SqlDataReader reader)
        {
            UserRole userRoles = new UserRole();
            userRoles.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            userRoles.RoleName = Convert.ToString(reader["RoleName"]);
            userRoles.RoleDescription = Convert.ToString(reader["RoleDescription"]);

            return userRoles;
        }
    }
}
