using System;
using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarYearSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchTypeEnum SearchType { get; private set; }

        public CarYearSearch(DateTime value, SearchTypeEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }
    }
}
