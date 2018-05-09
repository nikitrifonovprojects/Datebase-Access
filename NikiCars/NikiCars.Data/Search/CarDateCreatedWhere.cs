using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarDateCreatedWhere : Where
    {
        private const string PARAMETER = "CarDateCreatedWhere";
        private readonly CarDateCreatedSearch search;

        public CarDateCreatedWhere(CarDateCreatedSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (DateTime)this.search.Value;
            var result = string.Empty;

            SqlParameter sqlParameter = new SqlParameter() { Value = value, SqlDbType = System.Data.SqlDbType.SmallDateTime, ParameterName = PARAMETER };
            switch (this.search.SearchType)
            {
                case SearchTypeEnum.GreaterOrEquals:
                    result += ($"Cars.DateCreated >= @{PARAMETER}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result, new List<SqlParameter>() { sqlParameter });
        }
    }
}
