using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarConditionSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public CarConditionSearch(object value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }
    }
}
