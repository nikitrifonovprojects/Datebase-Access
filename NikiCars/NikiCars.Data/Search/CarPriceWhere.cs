using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
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
                case SearchTypeEnum.Equals:
                    result.Append($"Cars.Price = @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.NotEquals:
                    result.Append($"Cars.Price != @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.LessThan:
                    result.Append($"Cars.Price < @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.GreaterThan:
                    result.Append($"Cars.Price > @{PARAMETER + this.ParameterName}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result.ToString(), new List<SqlParameter>() { sqlParameter });
        }
    }
}
