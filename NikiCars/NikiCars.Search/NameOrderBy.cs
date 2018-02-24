using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class NameOrderBy : IOrderBy, IEntityOrderBy<CarMake>
    {
        public OrderByEnum OrderByType { get; private set; }

        public NameOrderBy() 
            : this(OrderByEnum.Ascending)
        {
            
        }

        public NameOrderBy(OrderByEnum orderBy)
        {
            this.OrderByType = orderBy;
        }
    }
}
