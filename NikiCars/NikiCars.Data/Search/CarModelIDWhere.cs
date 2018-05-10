using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Data.DataConstants;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarModelIDWhere : Where
    {
        private const string PARAMETER = "CarModelIDWhere";
        private readonly CarModelIDSearch search;

        public CarModelIDWhere(CarModelIDSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (List<int>)this.search.Value;
            var list = new List<SqlParameter>();
            var result = new StringBuilder();

            if (value.Count > 1)
            {
                result.Append("(");
            }

            for (int i = 0; i < value.Count; i++)
            {
                SqlParameter sqlParameter = new SqlParameter() { Value = value[i], ParameterName = PARAMETER + this.ParameterName + i };
                switch (this.search.SearchType)
                {
                    case SearchTypeEnum.Equals:
                        result.Append($"{DatabaseTableNames.CARS}.{CarColumns.CARMODEL_ID} = @{PARAMETER + this.ParameterName + i}");
                        break;
                    case SearchTypeEnum.NotEquals:
                        result.Append($"{DatabaseTableNames.CARS}.{CarColumns.CARMODEL_ID} != @{PARAMETER + this.ParameterName + i}");
                        break;
                    default:
                        throw new NotSupportedException();
                }

                list.Add(sqlParameter);

                if (i < value.Count - 1)
                {
                    result.Append(" OR ");
                }
            }

            if (value.Count > 1)
            {
                result.Append(")");
            }

            return Tuple.Create(result.ToString(), list);
        }
    }
}
