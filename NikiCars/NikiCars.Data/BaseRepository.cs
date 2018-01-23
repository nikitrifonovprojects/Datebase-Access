using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NikiCars.Data
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        private SqlConnection connection;
        private string createCommandText;
        private string deleteCommandText;
        private string updateCommandText;

        public BaseRepository()
        {
            this.connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            this.connection.Open();
        }

        protected abstract string GetTableName();

        protected abstract string GetPrimaryKeyName();

        protected abstract Dictionary<string, SqlParameter> GetCreateParameters(T item);


        protected abstract void MapPrimaryKey(T item, SqlDataReader reader);

        protected abstract string GetPrimaryKeyValue(T item);

        protected abstract T MapProperties(SqlDataReader reader);


        public T Create(T item)
        {
            var parameters = GetCreateParameters(item);
            if (string.IsNullOrEmpty(this.createCommandText))
            {
                StringBuilder columns = new StringBuilder();
                StringBuilder commandParams = new StringBuilder();

                foreach (var param in parameters)
                {
                    if (columns.Length > 0)
                    {
                        columns.Append(",");
                        commandParams.Append(",");
                    }

                    columns.Append(param.Key);
                    commandParams.Append("@" + param.Key);
                }

                this.createCommandText = "INSERT INTO " + GetTableName() + "(" + columns.ToString() + ")" + " OUTPUT INSERTED." + GetPrimaryKeyName() + " VALUES(" + commandParams.ToString() + ")";
            }

            SqlCommand command = new SqlCommand(this.createCommandText)
            {
                CommandType = CommandType.Text,
                Connection = connection
            };

            foreach (var param in parameters)
            {
                param.Value.ParameterName = "@" + param.Key;
                command.Parameters.Add(param.Value);
            }

            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                MapPrimaryKey(item, reader);
            }

            return item;
        }

        public void Delete(T item)
        {
            if (string.IsNullOrEmpty(this.deleteCommandText))
            {
                this.deleteCommandText = "DELETE FROM " + GetTableName() + " WHERE " + GetPrimaryKeyName() + " = @param1";
            }

            SqlCommand command = new SqlCommand(this.deleteCommandText)
            {
                CommandType = CommandType.Text,
                Connection = connection
            };

            command.Parameters.AddWithValue("@param1", GetPrimaryKeyValue(item));

            command.ExecuteNonQuery();
        }

        public List<T> GetAll(int pageNumber, int pageSize)
        {
            var list = new List<T>();
            SqlCommand command = new SqlCommand("SELECT * FROM " + GetTableName() + " ORDER BY " + GetPrimaryKeyName() + " OFFSET @param1 ROWS FETCH NEXT @param2 ROWS ONLY")
            {
                CommandType = CommandType.Text,
                Connection = connection
            };

            command.Parameters.AddWithValue("@param1", pageNumber * pageSize);
            command.Parameters.AddWithValue("@param2", pageSize);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(MapProperties(reader));
                }
            }

            return list;
        }

        public T GetByID(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM " + GetTableName() + " WHERE " + GetPrimaryKeyName() + " = @param1")
            {
                CommandType = CommandType.Text,
                Connection = connection
            };

            command.Parameters.AddWithValue("@param1", id);
            command.ExecuteNonQuery();


            var item = default(T);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    item = MapProperties(reader);
                }
            }

            return item;
        }

        public void Update(T item)
        {
            var parameters = GetCreateParameters(item);

            if (string.IsNullOrEmpty(this.updateCommandText))
            {
                StringBuilder commandParams = new StringBuilder();

                foreach (var param in parameters)
                {
                    if (commandParams.Length > 0)
                    {
                        commandParams.Append(",");
                    }

                    commandParams.Append(param.Key + " = " + "@" + param.Key);
                }
                
                this.updateCommandText = "UPDATE " + GetTableName() + " SET " + commandParams + " WHERE " + GetPrimaryKeyName() + " = @param1";
            }

            SqlCommand command = new SqlCommand(this.updateCommandText)
            {
                CommandType = CommandType.Text,
                Connection = connection
            };

            foreach (var param in parameters)
            {
                param.Value.ParameterName = "@" + param.Key;
                command.Parameters.Add(param.Value);
            }

            command.Parameters.AddWithValue("@param1", GetPrimaryKeyValue(item));
            command.ExecuteNonQuery();
        }

        public int Count()
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) AS [Count] FROM " + GetTableName())
            {
                CommandType = CommandType.Text,
                Connection = connection
            };

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}
