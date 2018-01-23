using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class CityRepository : BaseRepository<City>
    {
        private const string PRIMARY_KEY = "CityID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(City item)
        {
            return new Dictionary<string, SqlParameter>
            {
                { "Name", new SqlParameter() { Value = item.Name } },
                {"CountyID", new SqlParameter() { Value = item.CountyID.ToString() } }
            };
        }

        protected override string GetPrimaryKeyValue(City item)
        {
            return item.CityID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "City";
        }

        protected override void MapPrimaryKey(City item, SqlDataReader reader)
        {
            item.CityID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override City MapProperties(SqlDataReader reader)
        {
            City city = new City();
            city.CityID = Convert.ToInt32(reader[PRIMARY_KEY]);
            city.Name = Convert.ToString(reader["Name"]);
            city.CountyID = Convert.ToInt32(reader["CountryID"]);

            return city;
        }
    }
}
