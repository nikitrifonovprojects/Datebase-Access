using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NikiCars.Data.Search
{
    public abstract class Where : IWhere
    {
        protected string ParameterName { get => (this.GetHashCode() > 0 ? this.GetHashCode().ToString() : "_" + (this.GetHashCode() * (-1))) + "_"; }

        public virtual string GenerateJoin()
        {
            return string.Empty;
        }

        public abstract Tuple<string, List<SqlParameter>> GenerateWhereClause();
    }
}
