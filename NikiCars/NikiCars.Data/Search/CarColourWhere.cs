using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarColourWhere : Where
    {
        private const string PARAMETER = "CarColourWhere";
        private readonly CarColourSearch search;

        public CarColourWhere(CarColourSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (int)this.search.Value;
            var result = string.Empty;

            SqlParameter sqlParameter = new SqlParameter() { Value = value, ParameterName = PARAMETER };
            switch (this.search.SearchType)
            {
                case SearchTypeEnum.Equals:
                    result += ($"Cars.ColourID = @{PARAMETER}");
                    break;
                case SearchTypeEnum.NotEquals:
                    result += ($"Cars.ColourID =! @{PARAMETER}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result, new List<SqlParameter>() { sqlParameter });
        }
    }
}
