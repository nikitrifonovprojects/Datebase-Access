﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class CarCoupeRepository : BaseRepository<CarCoupe>
    {
        private const string PRIMARY_KEY = "CarCoupeID";

        protected override string GetTableName()
        {
            return "CarCoupe";
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(CarCoupe item)
        {
            return new Dictionary<string, SqlParameter>
            {
                { "Name", new SqlParameter() { Value = item.Name } }
            };
        }

        protected override void MapPrimaryKey(CarCoupe item, SqlDataReader reader)
        {
            item.CarCoupeID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override string GetPrimaryKeyValue(CarCoupe item)
        {
            return item.CarCoupeID.ToString();
        }

        protected override CarCoupe MapProperties(SqlDataReader reader)
        {
            CarCoupe carCoupe = new CarCoupe();
            carCoupe.CarCoupeID = Convert.ToInt32(reader[PRIMARY_KEY]);
            carCoupe.Name = Convert.ToString(reader["Name"]);

            return carCoupe;
        }
    }
}