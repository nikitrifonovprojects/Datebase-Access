using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    internal class NameWhere : Where
    {
        private const string PARAMETER_NAME = "NameWhere";
        private readonly NameSearch search;

        public NameWhere(NameSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = this.search.Value as string;

            SqlParameter sqlParameter = new SqlParameter() { Value = value, ParameterName = PARAMETER_NAME };

            string result = string.Empty;
            switch (this.search.SearchType)
            {
                case SearchEnum.Equals:
                    result = $"Name = @{PARAMETER_NAME}";
                    break;
                case SearchEnum.NotEquals:
                    result = $"Name != @{PARAMETER_NAME}";
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result, new List<SqlParameter>() { sqlParameter });
        }
    }
}
