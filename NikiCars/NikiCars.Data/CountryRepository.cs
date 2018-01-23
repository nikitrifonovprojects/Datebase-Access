using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class CountryRepository : BaseRepository<Country>
    {
        private const string PRIMARY_KEY = "CountryID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(Country item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Name", new SqlParameter() { Value = item.Name } }
            };
        }

        protected override string GetPrimaryKeyValue(Country item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "Counties";
        }

        protected override void MapPrimaryKey(Country item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override Country MapProperties(SqlDataReader reader)
        {
            Country country = new Country();
            country.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            country.Name = Convert.ToString(reader["Name"]);

            return country;
        }
    }
}
