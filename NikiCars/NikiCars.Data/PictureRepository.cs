using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public class PictureRepository : BaseRepository<Picture>
    {
        private const string PRIMARY_KEY = "PictureID";

        protected override Dictionary<string, SqlParameter> GetCreateParameters(Picture item)
        {
            return new Dictionary<string, SqlParameter>
            {
                {"PictureFile", new SqlParameter() { Value = item.PictureFile, SqlDbType = SqlDbType.VarBinary } },
                {"CarID", new SqlParameter() { Value = item.CarID.ToString() } }
            };
        }

        protected override string GetPrimaryKeyValue(Picture item)
        {
            return item.ID.ToString();
        }

        protected override string GetPrimaryKeyName()
        {
            return PRIMARY_KEY;
        }

        protected override string GetTableName()
        {
            return "Pictures";
        }

        protected override void MapPrimaryKey(Picture item, SqlDataReader reader)
        {
            item.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
        }

        protected override Picture MapProperties(SqlDataReader reader)
        {
            Picture picture = new Picture();
            picture.ID = Convert.ToInt32(reader[PRIMARY_KEY]);
            picture.PictureFile = Encoding.ASCII.GetBytes(reader["PictureFile"].ToString());     
            picture.CarID = Convert.ToInt32(reader["CarID"]);

            return picture;
        }
    }
}
