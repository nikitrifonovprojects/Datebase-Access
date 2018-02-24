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
                case NameSearch r:
                    var nameWhere = new NameWhere(r);
                    return nameWhere;
                case CarMakeIDSearch s:
                    var res = new CarMakeIDWhere(s);
                    return res;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
