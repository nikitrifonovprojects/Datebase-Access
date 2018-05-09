﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarKilometersWhere : Where
    {
        private const string PARAMETER = "CarKilometersWhere";
        private readonly CarKilometersSearch search;

        public CarKilometersWhere(CarKilometersSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (int)this.search.Value;
            var result = string.Empty;

            SqlParameter sqlParameter = new SqlParameter() { Value = value, ParameterName = PARAMETER };
            switch (this.search.SearchType)
            {
                case SearchTypeEnum.GreaterThan:
                    result += ($"Cars.Kilometers > @{PARAMETER}");
                    break;
                case SearchTypeEnum.LessThan:
                    result += ($"Cars.Kilometers < @{PARAMETER}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result, new List<SqlParameter>() { sqlParameter });
        }
    }
}
