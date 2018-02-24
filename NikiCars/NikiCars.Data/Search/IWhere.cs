using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    internal interface IWhere
    {
        Tuple<string, List<SqlParameter>> GenerateWhereClause();
    }
}
