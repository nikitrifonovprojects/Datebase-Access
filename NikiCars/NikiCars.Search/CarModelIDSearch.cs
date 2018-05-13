using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarModelIDSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public CarModelIDSearch(List<int> value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }

        public CarModelIDSearch(List<int> value) 
            : this(value, SearchEnum.Equals)
        {

        }

        public CarModelIDSearch(int value, SearchEnum searchType)
            : this(new List<int>() { value }, searchType)
        {

        }

        public CarModelIDSearch(int value)
            : this(new List<int>() { value }, SearchEnum.Equals)
        {

        }
    }
}
