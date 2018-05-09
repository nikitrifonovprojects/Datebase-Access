using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarEngineSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchTypeEnum SearchType { get; private set; }

        public CarEngineSearch(int value, SearchTypeEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }
    }
}
