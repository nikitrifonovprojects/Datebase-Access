using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class NameSearch : ISearch, IEntitySearch<CarMake>, IEntitySearch<CarModel>, IEntitySearch<Extra>
    {
        public object Value { get; private set; }

        public SearchEnum SearchType { get; private set; }

        public NameSearch(string value, SearchEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }

        public NameSearch(string value) 
            : this(value, SearchEnum.Equals)
        {

        }
    }
}
