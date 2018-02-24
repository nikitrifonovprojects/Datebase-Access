using System;
using NikiCars.Search;
using NikiCars.Search.Interfaces;

namespace NikiCars.Data.Search
{
    internal static class WhereFactory
    {
        public static IWhere CreateWhereClause(ISearch search)
        {
            switch (search)
            {
                case NameSearch s:
                    var res = new NameWhere(s);
                    return res;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
