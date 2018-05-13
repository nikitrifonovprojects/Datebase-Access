using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarMakeIDSearch : ISearch, IEntitySearch<CarMake>, IEntitySearch<CarModel>, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public CarMakeIDSearch(List<int> value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }

        public CarMakeIDSearch(List<int> value) 
            : this(value, SearchEnum.Equals)
        {

        }

        public CarMakeIDSearch(int value, SearchEnum searchType) 
            : this(new List<int>() { value }, searchType)
        {

        }

        public CarMakeIDSearch(int value)
            : this(new List<int>() { value }, SearchEnum.Equals)
        {

        }
    }
}
