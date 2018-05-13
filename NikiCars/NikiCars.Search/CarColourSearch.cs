using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarColourSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public CarColourSearch(int value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }

        public CarColourSearch(int value) 
            : this(value, SearchEnum.Equals)
        {

        }
    }
}
