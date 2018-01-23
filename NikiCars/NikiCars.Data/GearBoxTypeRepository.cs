using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class GearBoxTypeRepository : BaseRepository<GearBoxType>
    {
        private const string PRIMARY_KEY = "GearboxTypeID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(GearBoxType item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Type", new SqlParameter() { Value = item.Type } }
            };
        }

        protected override string GetPrimaryKeyValue(GearBoxType item)
        {
            return item.GearboxTypeID.ToString();
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
            item.GearboxTypeID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override GearBoxType MapProperties(SqlDataReader reader)
        {
            GearBoxType gearBoxType = new GearBoxType();
            gearBoxType.GearboxTypeID = Convert.ToInt32(reader[PRIMARY_KEY]);
            gearBoxType.Type = Convert.ToString(reader["Type"]);

            return gearBoxType;
        }
    }
}
