using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarPriceOrderBy : IOrderBy, IEntityOrderBy<Car>
    {
        public OrderByEnum OrderByType { get; private set; }

        public CarPriceOrderBy()
            : this(OrderByEnum.Ascending)
        {

        }

        public CarPriceOrderBy(OrderByEnum orderBy)
        {
            this.OrderByType = orderBy;
        }
    }
}
