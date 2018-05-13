using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarIDSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public CarIDSearch(int value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }

        public CarIDSearch(int value) 
            : this(value, SearchEnum.Equals)
        {

        }
    }
}
