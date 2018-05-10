using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Data.DataConstants;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarEngineWhere : Where
    {
        private const string PARAMETER = "CarEngineWhere";
        private readonly CarEngineSearch search;

        public CarEngineWhere(CarEngineSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (int)this.search.Value;
            var list = new List<SqlParameter>();
            var result = new StringBuilder();

            SqlParameter sqlParameter = new SqlParameter() { Value = value, ParameterName = PARAMETER + this.ParameterName };
            switch (this.search.SearchType)
            {
                case SearchTypeEnum.Equals:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.ENGINE_CAPACITY} = @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.NotEquals:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.ENGINE_CAPACITY} != @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.LessThan:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.ENGINE_CAPACITY} < @{PARAMETER + this.ParameterName}");
                    break;
                case SearchTypeEnum.GreaterThan:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.ENGINE_CAPACITY} > @{PARAMETER + this.ParameterName}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result.ToString(), new List<SqlParameter>() { sqlParameter });
        }
    }
}
