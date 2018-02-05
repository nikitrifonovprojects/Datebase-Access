using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class ColourRepository : BaseRepository<Colour>
    {
        private const string PRIMARY_KEY = "ColourID";

        public ColourRepository(IConfig config) 
            : base(config)
        {
        }

        protected override Dictionary<string, SqlParameter> GetCreateParameters(Colour item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"Name", new SqlParameter() { Value = item.Name } }
            };
        }

        protected override string GetPrimaryKeyValue(Colour item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "Colours";
        }

        protected override void MapPrimaryKey(Colour item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override Colour MapProperties(SqlDataReader reader)
        {
            Colour colour = new Colour();
            colour.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            colour.Name = Convert.ToString(reader["Name"]);

            return colour;
        }
    }
}
