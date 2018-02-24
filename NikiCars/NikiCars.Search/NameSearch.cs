using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class NameSearch : ISearch, IEntitySearch<CarMake>, IEntitySearch<CarModel>
    {
        public object Value { get; private set; }

        public SearchTypeEnum SearchType { get; private set; }

        public NameSearch(string value, SearchTypeEnum searchType)
        {
            this.Value = value;
            this.SearchType = searchType;
        }
    }
}
