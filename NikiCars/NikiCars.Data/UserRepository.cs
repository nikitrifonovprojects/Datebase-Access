using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;

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
            user.Type = Convert.ToString(reader["Type"]);
            user.Bulstat = Convert.ToString(reader["Bulstat"]);
            user.Website = Convert.ToString(reader["Website"]);
            user.PageName = Convert.ToString(reader["PageName"]);
            user.IsOfficialImporter = Convert.ToBoolean(reader["IsOfficialImporter"]);
            user.IsOrganisation = Convert.ToBoolean(reader["IsOrganisation"]);

            return user;
        }

        public User GetUserByName(string loginName, List<UserIncludes> list = null)
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

            ProcessIncludes(user, list);

            return user;
        }

        public User GetUserByEmail(string email, List<UserIncludes> list = null)
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

        private void ProcessIncludes(User user, List<UserIncludes> list)
        {
            if (user != null && list != null)
            {
                if (list.Contains(UserIncludes.UserRoles))
                {
                    user.Roles = GetUserRoles(user.ID);
                }
            }
        }

        private List<UserRole> GetUserRoles(int userId)
        {
            SqlCommand command = new SqlCommand("SELECT usr.RoleID, usr.RoleName, usr.RoleDescription" +
                                                " FROM UserRoles usr" +
                                                " INNER JOIN Users_UserRoles ur" +
                                                " ON ur.RoleID = usr.RoleID" +
                                                " INNER JOIN  Users u" +
                                                " ON u.UserID = ur.UserID" +
                                                " WHERE u.UserID = @param1")
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            command.Parameters.AddWithValue("@param1", userId);
            command.ExecuteNonQuery();

            List<UserRole> list = new List<UserRole>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var role = UserRoleRepository.StaticMapProperties(reader);
                    list.Add(role);
                }
            }

            return list;
        }

        protected override User UpdateTableRelationships(User item)
        {
            if (item.Roles == null)
            {
                return item;
            }

            var roles = GetUserRoles(item.ID);
            var idToRemove = roles.Select(a => a.ID).Except(item.Roles.Select(c => c.ID)).ToList();
            var idToAdd = item.Roles.Select(a => a.ID).Except(roles.Select(c => c.ID)).ToList();

            if (idToRemove.Count > 0 || idToAdd.Count > 0)
            {
                if (idToRemove.Count > 0)
                {
                    Dictionary<string, int> parameters = new Dictionary<string, int>();
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < idToRemove.Count; i++)
                    {
                        builder.Append($"@param{i + 2},");
                        parameters.Add($"@param{i + 2}", idToRemove[i]);
                    }

                    string stringParam = builder.ToString().TrimEnd(new char[] { ',' });

                    SqlCommand command = new SqlCommand("DELETE FROM Users_UserRoles WHERE UserID = @param1 AND RoleID IN (" + stringParam + ")")
                    {
                        CommandType = CommandType.Text,
                        Connection = Connection
                    };

                    command.Parameters.AddWithValue("@param1", item.ID);
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    command.ExecuteNonQuery();
                }

                if (idToAdd.Count > 0)
                {
                    for (int i = 0; i < idToAdd.Count; i++)
                    {
                        SqlCommand command = new SqlCommand("INSERT INTO Users_UserRoles(UserID,RoleID) VALUES(@param1,@param2)")
                        {
                            CommandType = CommandType.Text,
                            Connection = Connection
                        };

                        command.Parameters.AddWithValue("@param1", item.ID);
                        command.Parameters.AddWithValue("@param2", idToAdd[i]);
                        command.ExecuteNonQuery();
                    }
                }
            }

            return item;
        }

        public User GetUserByID(int id, List<UserIncludes> list = null)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE UserID = @param1")
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            command.Parameters.AddWithValue("@param1", id);

            User user = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = MapProperties(reader);
                }
            }

            ProcessIncludes(user, list);

            return user;
        }
    }
}
