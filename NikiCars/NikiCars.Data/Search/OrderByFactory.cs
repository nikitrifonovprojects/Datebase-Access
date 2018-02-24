using System;
using NikiCars.Search;
using NikiCars.Search.Interfaces;

namespace NikiCars.Data.Search
{
    internal static class OrderByFactory
    {
        public static IOrderByClause CreateOrderByClause(IOrderBy orderBy)
        {
            switch (orderBy)
            {
                case NameOrderBy s:
                    return new NameOrderByClause(s.OrderByType);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
