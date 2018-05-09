using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarPriceSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchTypeEnum SearchType { get; private set; }

        public CarPriceSearch(decimal value, SearchTypeEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }
    }
}
