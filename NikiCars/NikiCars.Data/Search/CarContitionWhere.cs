using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
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
            var value = (Dictionary<string, bool>)this.search.Value;
            var list = new List<SqlParameter>();
            var result = new StringBuilder();

            if (value.ContainsKey("IsUsed"))
            {
                if (value["IsUsed"])
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
            if (value.ContainsKey("IsForParts"))
            {
                if (value["IsForParts"])
                {
                    var sqlParameter = new SqlParameter() { Value = 1, ParameterName = PARAMETER + 1};
                    list.Add(sqlParameter);
                }
                else
                {
                    var sqlParameter = new SqlParameter() { Value = 0, ParameterName = PARAMETER + 1};
                    list.Add(sqlParameter);
                }

                if (value["IsUsed"])
                {
                    result.Append($" AND Cars.IsForParts = @{PARAMETER + 1}");
                }
                else
                {
                    result.Append($"Cars.IsForParts = @{PARAMETER + 1}");
                }
            }
            if (value.ContainsKey("IsLeftSteering"))
            {
                if (value["IsLeftSteering"])
                {
                    var sqlParameter = new SqlParameter() { Value = 1, ParameterName = PARAMETER + 2 };
                    list.Add(sqlParameter);
                }
                else
                {
                    var sqlParameter = new SqlParameter() { Value = 0, ParameterName = PARAMETER + 2 };
                    list.Add(sqlParameter);
                }

                if (value["IsUsed"] || value["IsForParts"])
                {
                    result.Append($" AND Cars.IsLeftSteering = @{PARAMETER + 2}");
                }
                else
                {
                    result.Append($"Cars.IsLeftSteering = @{PARAMETER + 2}");
                }
            }


            return Tuple.Create(result.ToString(), list);
        }
    }
}
