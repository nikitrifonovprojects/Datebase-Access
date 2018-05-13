using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Models.CarModels;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarContitionWhere : Where
    {
        private const string PARAMETER = "CarConditionWhere";
        private readonly CarConditionSearch search;

        public CarContitionWhere(CarConditionSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (CarContitionModel)this.search.Value;
            var list = new List<SqlParameter>();
            var result = new StringBuilder();

            if (value.IsUsed.HasValue)
            {
                if (value.IsUsed == true)
                {
                    var sqlParameter = new SqlParameter() { Value = 1, ParameterName = PARAMETER };
                    list.Add(sqlParameter);
                }
                else
                {
                    var sqlParameter = new SqlParameter() { Value = 0, ParameterName = PARAMETER };
                    list.Add(sqlParameter);
                }

                result.Append($"Cars.IsUsed = @{PARAMETER}");
            }
            if (value.IsForParts.HasValue)
            {
                if (value.IsForParts == true)
                {
                    var sqlParameter = new SqlParameter() { Value = 1, ParameterName = PARAMETER + 1};
                    list.Add(sqlParameter);
                }
                else
                {
                    var sqlParameter = new SqlParameter() { Value = 0, ParameterName = PARAMETER + 1};
                    list.Add(sqlParameter);
                }

                if (value.IsUsed == true)
                {
                    result.Append($" AND Cars.IsForParts = @{PARAMETER + 1}");
                }
                else
                {
                    result.Append($"Cars.IsForParts = @{PARAMETER + 1}");
                }
            }

            return Tuple.Create(result.ToString(), list);
        }
    }
}
