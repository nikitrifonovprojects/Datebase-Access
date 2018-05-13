using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Data.DataConstants;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarExtrasWhere : Where
    {
        private const string PARAMETER = "CarExtraWhere";
        private readonly CarExtrasSearch search;

        public CarExtrasWhere(CarExtrasSearch search)
        {
            this.search = search;
        }

        public override string GenerateJoin()
        {
            return $" INNER JOIN {DatabaseTableNames.CARS_EXTRAS} ON {DatabaseTableNames.CARS}.{CarColumns.CAR_ID} = {DatabaseTableNames.CARS_EXTRAS}.{Cars_CarExtrasColumns.CAR_ID} ";
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (List<int>)this.search.Value;
            var list = new List<SqlParameter>();
            var result = new StringBuilder();

            for (int i = 0; i < value.Count; i++)
            {
                SqlParameter sqlParameter = new SqlParameter() { Value = value[i], ParameterName = PARAMETER + this.ParameterName + i};
                switch (this.search.SearchType)
                {
                    case SearchEnum.Equals:
                        result.Append($"{DatabaseTableNames.CARS_EXTRAS}.{Cars_CarExtrasColumns.CAR_EXTRA_ID} = @{PARAMETER + this.ParameterName + i}");
                        break;
                    case SearchEnum.NotEquals:
                        result.Append($"{DatabaseTableNames.CARS_EXTRAS}.{Cars_CarExtrasColumns.CAR_EXTRA_ID} != @{PARAMETER + this.ParameterName + i}");
                        break;
                    default:
                        throw new NotSupportedException();
                }

                list.Add(sqlParameter);

                if (i < value.Count -1)
                {
                    result.Append(" AND ");
                }
            }

            return Tuple.Create(result.ToString(), list);
        }
    }
}
