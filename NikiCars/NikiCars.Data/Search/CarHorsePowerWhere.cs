using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.DataConstants;
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
                case SearchEnum.GreaterThan:
                    result += ($"{DatabaseTableNames.CARS}.{CarColumns.HORSEPOWER} > @{PARAMETER}");
                    break;
                case SearchEnum.LessThan:
                    result += ($"{DatabaseTableNames.CARS}.{CarColumns.HORSEPOWER} < @{PARAMETER}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result, new List<SqlParameter>() { sqlParameter });
        }
    }
}
