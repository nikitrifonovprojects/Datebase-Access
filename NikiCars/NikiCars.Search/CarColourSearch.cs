﻿using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarColourSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchTypeEnum SearchType { get; private set; }

        public CarColourSearch(int value, SearchTypeEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }
    }
}