using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class FuelTypeRepository : BaseRepository<FuelType>
    {
        private const string PRIMARY_KEY = "FuelTypeID";

        public FuelTypeRepository(IConfig config) 
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(FuelType item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Name", new SqlParameter() { Value = item.Name } }
            };
        }

        protected override string GetPrimaryKeyValue(FuelType item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "FuelTypes";
        }

        protected override void MapPrimaryKey(FuelType item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override FuelType MapProperties(SqlDataReader reader)
        {
            FuelType fuelType = new FuelType();
            fuelType.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            fuelType.Name = Convert.ToString(reader["Name"]);

            return fuelType;
        }
    }
}
