using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarHorsePowerWhere : Where
    {
        private const string PARAMETER = "CarHorsePowerSearchWhere";
        private readonly CarHorsePowerSearch search;

        public CarHorsePowerWhere(CarHorsePowerSearch search)
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
                case SearchTypeEnum.GreaterThan:
                    result += ($"Cars.HorsePower > @{PARAMETER}");
                    break;
                case SearchTypeEnum.LessThan:
                    result += ($"Cars.HorsePower < @{PARAMETER}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result, new List<SqlParameter>() { sqlParameter });
        }
    }
}
