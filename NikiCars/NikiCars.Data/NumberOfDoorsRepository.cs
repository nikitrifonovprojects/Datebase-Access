using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class NumberOfDoorsRepository : BaseRepository<NumberOfDoors>
    {
        private const string PRIMARY_KEY = "NumberOfDoorsID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(NumberOfDoors item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"DoorCount", new SqlParameter() { Value = item.DoorCount } }
            };
        }

        protected override string GetPrimaryKeyValue(NumberOfDoors item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "NumberOfDoors";
        }

        protected override void MapPrimaryKey(NumberOfDoors item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override NumberOfDoors MapProperties(SqlDataReader reader)
        {
            NumberOfDoors doors = new NumberOfDoors();
            doors.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            doors.DoorCount = Convert.ToString(reader["DoorCount"]);

            return doors;
        }
    }
}
