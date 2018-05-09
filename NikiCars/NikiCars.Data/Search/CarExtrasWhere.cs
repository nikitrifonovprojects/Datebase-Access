﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
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
            return " INNER JOIN Cars_CarExtras ON Cars.CarID = Cars_CarExtras.CarID ";
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
                    case SearchTypeEnum.Equals:
                        result.Append($"Cars_CarExtras.CarExtraID = @{PARAMETER + this.ParameterName + i}");
                        break;
                    case SearchTypeEnum.NotEquals:
                        result.Append($"Cars_CarExtras.CarExtraID != @{PARAMETER + this.ParameterName + i}");
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