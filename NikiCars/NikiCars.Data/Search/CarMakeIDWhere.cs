using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarMakeIDWhere : IWhere
    {
        private const string PARAMETER_NAME = "CarMakeIDWhere";
        private readonly CarMakeIDSearch search;

        public CarMakeIDWhere(CarMakeIDSearch search)
        {
            this.search = search;
        }

        public Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (int)this.search.Value;

            SqlParameter sqlParameter = new SqlParameter() { Value = value, ParameterName = PARAMETER_NAME };

            string result = string.Empty;
            switch (this.search.SearchType)
            {
                case SearchTypeEnum.Equals:
                    result = $"CarMakeID = @{PARAMETER_NAME}";
                    break;
                case SearchTypeEnum.NotEquals:
                    result = $"CarMakeID != @{PARAMETER_NAME}";
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result, new List<SqlParameter>() { sqlParameter });
        }
    }
}
