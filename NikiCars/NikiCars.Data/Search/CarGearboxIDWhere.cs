using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NikiCars.Data.DataConstants;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarGearboxIDWhere : Where
    {
        private const string PARAMETER = "CarGearboxIDWhere";
        private readonly CarGearboxIDSearch search;

        public CarGearboxIDWhere(CarGearboxIDSearch search)
        {
            this.search = search;
        }

        public override Tuple<string, List<SqlParameter>> GenerateWhereClause()
        {
            var value = (int)this.search.Value;
            var result = new StringBuilder();

            SqlParameter sqlParameter = new SqlParameter() { Value = value, ParameterName = PARAMETER + this.ParameterName };
            switch (this.search.SearchType)
            {
                case SearchEnum.Equals:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.GEARBOX_TYPE_ID} = @{PARAMETER + this.ParameterName}");
                    break;
                case SearchEnum.NotEquals:
                    result.Append($"{DatabaseTableNames.CARS}.{CarColumns.GEARBOX_TYPE_ID} != @{PARAMETER + this.ParameterName}");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return Tuple.Create(result.ToString(), new List<SqlParameter>() { sqlParameter });
        }
    }
}
