using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarYearWhere : Where
    {
        private const string PARAMETER = "CarYearWhere";
        private readonly CarYearSearch search;

        public CarYearWhere(CarYearSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (DateTime)this.search.Value;
            var result = new StringBuilder();

            SqlParameter sqlParameter = new SqlParameter() { Value = value, ParameterName = PARAMETER + this.ParameterName };
            switch (this.search.SearchType)
            {
                case SearchTypeEnum.Equals:
                    result.Append($"Cars.DateCreated = @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.NotEquals:
                    result.Append($"Cars.DateCreated != @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.LessThan:
                    result.Append($"Cars.DateCreated < @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.GreaterThan:
                    result.Append($"Cars.DateCreated > @{PARAMETER + this.ParameterName}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result.ToString(), new List<SqlParameter>() { sqlParameter });
        }
    }
}
