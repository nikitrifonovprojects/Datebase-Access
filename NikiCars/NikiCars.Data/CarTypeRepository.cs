using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class CarTypeRepository : BaseRepository<CarType>
    {
        private const string PRIMARY_KEY = "CarTypeID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(CarType item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Name", new SqlParameter() { Value = item.Name } }
            };
        }

        protected override string GetPrimaryKeyValue(CarType item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "CarTypes";
        }

        protected override void MapPrimaryKey(CarType item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override CarType MapProperties(SqlDataReader reader)
        {
            CarType carType = new CarType();
            carType.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            carType.Name = Convert.ToString(reader["Name"]);

            return carType;
        }
    }
}
