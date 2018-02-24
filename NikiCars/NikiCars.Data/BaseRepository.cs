using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Interfaces;
using NikiCars.Data.Search;
using NikiCars.Search;
using NikiCars.Search.Interfaces;

namespace NikiCars.Data
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        private string createCommandText;
        private string deleteCommandText;
        private string updateCommandText;

        protected SqlConnection Connection { get; private set; }

        public BaseRepository(IConfig config)
        {
            this.Connection = new SqlConnection(config.ConnectionString);
            this.Connection.Open();
        }

        protected abstract string GetTableName();

        protected abstract string GetPrimaryKeyName();

        protected abstract Dictionary<string, SqlParameter> GetCreateParameters(T item);


        protected abstract void MapPrimaryKey(T item, SqlDataReader reader);

        protected abstract string GetPrimaryKeyValue(T item);

        protected abstract T MapProperties(SqlDataReader reader);

        protected virtual T UpdateTableRelationships(T item)
        {
            return item;
        }


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
                Connection = Connection
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

        public bool Delete(T item)
        {
            if (string.IsNullOrEmpty(this.deleteCommandText))
            {
                this.deleteCommandText = "DELETE FROM " + GetTableName() + " WHERE " + GetPrimaryKeyName() + " = @param1";
            }

            SqlCommand command = new SqlCommand(this.deleteCommandText)
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            command.Parameters.AddWithValue("@param1", GetPrimaryKeyValue(item));

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public List<T> GetAll(Pagination pagination)
        {
            return GetAll(null, null, pagination);
        }

        public List<T> GetAll()
        {
            return GetAll(null, null, null);
        }

        public List<T> GetAll(List<IEntitySearch<T>> search)
        {
            return GetAll(search, null, null);
        }

        public List<T> GetAll(List<IEntitySearch<T>> search, List<IEntityOrderBy<T>> order, Pagination pagination)
        {
            SqlCommand command = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM " + GetTableName());

            ApplyWhere(builder, search, command);
            ApplyOrderBy(builder, order);
            ApplyPaging(builder, command, pagination);

            command.CommandText = builder.ToString();

            var resultList = new List<T>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    resultList.Add(MapProperties(reader));
                }
            }

            return resultList;
        }

        private void ApplyWhere(StringBuilder builder, List<IEntitySearch<T>> search, SqlCommand command)
        {
            var whereClauses = new List<Tuple<string, List<SqlParameter>>>();
            
            if (search != null)
            {
                foreach (var item in search)
                {
                    var res = WhereFactory.CreateWhereClause(item);
                    whereClauses.Add(res.GenerateWhereClause());
                }
            }

            if (whereClauses.Count > 0)
            {
                builder.Append(" WHERE ");
                for (int i = 0; i < whereClauses.Count; i++)
                {
                    if (whereClauses.Count > 1)
                    {
                        builder.Append(" AND ");
                    }

                    builder.Append(whereClauses[i].Item1);
                    command.Parameters.Add(whereClauses[i].Item2[0]);
                }
            }
        }

        private void ApplyOrderBy(StringBuilder builder, List<IEntityOrderBy<T>> order)
        {
            var orderByClauses = new List<IOrderByClause>();

            if (order != null)
            {
                foreach (var item in order)
                {
                    var res = OrderByFactory.CreateOrderByClause(item);
                    orderByClauses.Add(res);
                }
            }

            if (orderByClauses.Count > 0)
            {
                builder.Append(" ORDER BY ");
                for (int i = 0; i < orderByClauses.Count; i++)
                {
                    if (orderByClauses.Count > 1)
                    {
                        builder.Append(" , ");
                    }

                    builder.Append(orderByClauses[i].GenerateOrderByClause());
                }
            }
        }

        private void ApplyPaging(StringBuilder builder, SqlCommand command, Pagination pagination)
        {
            if (pagination != null)
            {
                builder.Append(" OFFSET @number ROWS FETCH NEXT @size ROWS ONLY");
                command.Parameters.AddWithValue("@number", pagination.PageNumber * pagination.PageSize);
                command.Parameters.AddWithValue("@size", pagination.PageSize);
            }
        }

        public T GetByID(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM " + GetTableName() + " WHERE " + GetPrimaryKeyName() + " = @param1")
            {
                CommandType = CommandType.Text,
                Connection = Connection
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

        public T Update(T item)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    item = UpdateTableRelationships(item);
                    item = UpdateItem(item);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            return item;
        }

        private T UpdateItem(T item)
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
                Connection = Connection
            };

            foreach (var param in parameters)
            {
                param.Value.ParameterName = "@" + param.Key;
                command.Parameters.Add(param.Value);
            }

            command.Parameters.AddWithValue("@param1", GetPrimaryKeyValue(item));
            command.ExecuteNonQuery();

            return item;
        }

        public int Count()
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) AS [Count] FROM " + GetTableName())
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void Dispose()
        {
            this.Connection.Dispose();
        }
    }
}
