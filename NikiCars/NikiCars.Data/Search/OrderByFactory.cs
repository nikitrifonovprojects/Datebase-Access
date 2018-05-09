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
                case DateOrderBy s:
                    return new DateOrderByClause(s.OrderByType);
                case CarPriceOrderBy s:
                    return new CarPriceOrderByClause(s.OrderByType);
                case CarRegistrationOrderBy s:
                    return new CarRegistrationOrderByClause(s.OrderByType);
                case CarKilometersOrderBy s:
                    return new CarKilometersOrderByClause(s.OrderByType);
                case CarMakeModelPriceOrderBy s:
                    return new CarMakeModelPriceOrderByClause(s.OrderByType);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
