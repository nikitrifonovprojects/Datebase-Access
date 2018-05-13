using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;
using NikiCars.Search.SearchModels;

namespace NikiCars.Search
{
    public class CarConditionSearch : ISearch, IEntitySearch<Car>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public CarConditionSearch(CarContitionModel value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }

        public CarConditionSearch(CarContitionModel value) 
            : this(value, SearchEnum.Equals)
        {
        }
    }
}
