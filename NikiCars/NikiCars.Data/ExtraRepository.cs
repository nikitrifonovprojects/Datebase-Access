using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class ExtraRepository : BaseRepository<Extra>
    {
        private const string PRIMARY_KEY = "CarExtraID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(Extra item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Name", new SqlParameter() { Value = item.Name } }
            };
        }

        protected override string GetPrimaryKeyValue(Extra item)
        {
            return item.CarExtraID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "CarExtras";
        }

        protected override void MapPrimaryKey(Extra item, SqlDataReader reader)
        {
            item.CarExtraID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override Extra MapProperties(SqlDataReader reader)
        {
            Extra extra = new Extra();
            extra.CarExtraID = Convert.ToInt32(reader[PRIMARY_KEY]);
            extra.Name = Convert.ToString(reader["Name"]);

            return extra;
        }
    }
}
