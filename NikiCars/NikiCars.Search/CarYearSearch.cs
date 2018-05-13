using System;
using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarYearSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public CarYearSearch(DateTime value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }

        public CarYearSearch(DateTime value) 
            : this(value, SearchEnum.Equals)
        {

        }
    }
}
