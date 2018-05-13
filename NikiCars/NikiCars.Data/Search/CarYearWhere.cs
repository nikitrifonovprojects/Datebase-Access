using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Data.DataConstants;
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
                case SearchEnum.Equals:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.DATE_CREATED} = @{PARAMETER + this.ParameterName}");
                    break;
                case SearchEnum.NotEquals:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.DATE_CREATED} != @{PARAMETER + this.ParameterName}");
                    break;
                case SearchEnum.LessThan:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.DATE_CREATED} < @{PARAMETER + this.ParameterName}");
                    break;
                case SearchEnum.GreaterThan:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.DATE_CREATED} > @{PARAMETER + this.ParameterName}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result.ToString(), new List<SqlParameter>() { sqlParameter });
        }
    }
}
