using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarEngineSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public CarEngineSearch(int value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }

        public CarEngineSearch(int value) 
            : this(value, SearchEnum.Equals)
        {

        }
    }
}
