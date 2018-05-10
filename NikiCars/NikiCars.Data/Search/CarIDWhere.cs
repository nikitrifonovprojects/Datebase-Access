using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Data.DataConstants;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarIDWhere : Where
    {
        private const string PARAMETER = "CarIDWhere";
        private readonly CarIDSearch search;

        public CarIDWhere(CarIDSearch search)
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
                    result += ($"{DatabaseTableNames.CARS}.{CarColumns.CAR_ID} = @{PARAMETER}");
                    break;
                case SearchTypeEnum.NotEquals:
                    result += ($"{DatabaseTableNames.CARS}.{CarColumns.CAR_ID} != @{PARAMETER}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result, new List<SqlParameter>() { sqlParameter });
        }
    }
}
