using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class CarMakeRepository : BaseRepository<CarMake>
    {
        private const string PRIMARY_KEY = "CarMakeID";

        public CarMakeRepository(IConfig config) 
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(CarMake item)
        {
            return new Dictionary<string, SqlParameter>
            {
                { "Name",new SqlParameter() { Value = item.Name } },
                { "Country",new SqlParameter() { Value = item.Country } }
            };
        }

        protected override string GetPrimaryKeyValue(CarMake item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "CarMakes";
        }

        protected override void MapPrimaryKey(CarMake item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override CarMake MapProperties(SqlDataReader reader)
        {
            CarMake carMake = new CarMake();
            carMake.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            carMake.Name = Convert.ToString(reader["Name"]);
            carMake.Country = Convert.ToString(reader["Country"]);

            return carMake;
        }
    }
}
