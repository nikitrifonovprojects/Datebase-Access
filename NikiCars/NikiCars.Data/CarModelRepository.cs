using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class CarModelRepository : BaseRepository<CarModel>
    {
        private const string PRIMARY_KEY = "CarModelID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(CarModel item)
        {
            return new Dictionary<string, SqlParameter>
            {
                { "Name", new SqlParameter() { Value = item.Name } },
                {"CarMakeID", new SqlParameter() { Value = item.CarMakeID.ToString() } },
                {"CarTypeID", new SqlParameter() { Value = item.CarTypeID.ToString() } }
            };
        }

        protected override string GetPrimaryKeyValue(CarModel item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "CarModels";
        }

        protected override void MapPrimaryKey(CarModel item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override CarModel MapProperties(SqlDataReader reader)
        {
            CarModel carModel = new CarModel();
            carModel.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            carModel.Name = Convert.ToString(reader["Name"]);
            carModel.CarMakeID = Convert.ToInt32(reader["CarMakeID"]);
            carModel.CarTypeID = Convert.ToInt32(reader["CarTypeID"]);

            return carModel;
        }
    }
}
