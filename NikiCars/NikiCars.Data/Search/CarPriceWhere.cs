using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Data.DataConstants;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarPriceWhere : Where
    {
        private const string PARAMETER = "CarPriceWhere";
        private readonly CarPriceSearch search;

        public CarPriceWhere(CarPriceSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (decimal)this.search.Value;
            var result = new StringBuilder();

            SqlParameter sqlParameter = new SqlParameter() { Value = value, ParameterName = PARAMETER + this.ParameterName };
            switch (this.search.SearchType)
            {
                case SearchEnum.Equals:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.PRICE} = @{PARAMETER + this.ParameterName}");
                    break;
                case SearchEnum.NotEquals:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.PRICE} != @{PARAMETER + this.ParameterName}");
                    break;
                case SearchEnum.LessThan:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.PRICE} < @{PARAMETER + this.ParameterName}");
                    break;
                case SearchEnum.GreaterThan:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.PRICE} > @{PARAMETER + this.ParameterName}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result.ToString(), new List<SqlParameter>() { sqlParameter });
        }
    }
}
