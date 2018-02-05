using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class GearBoxTypeRepository : BaseRepository<GearBoxType>
    {
        private const string PRIMARY_KEY = "GearboxTypeID";

        public GearBoxTypeRepository(IConfig config) 
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(GearBoxType item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Type", new SqlParameter() { Value = item.Type } }
            };
        }

        protected override string GetPrimaryKeyValue(GearBoxType item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "GearboxTypes";
        }

        protected override void MapPrimaryKey(GearBoxType item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override GearBoxType MapProperties(SqlDataReader reader)
        {
            GearBoxType gearBoxType = new GearBoxType();
            gearBoxType.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            gearBoxType.Type = Convert.ToString(reader["Type"]);

            return gearBoxType;
        }
    }
}
