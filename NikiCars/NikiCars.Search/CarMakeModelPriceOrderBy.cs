using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarMakeModelPriceOrderBy : IOrderBy, IEntityOrderBy<Car>
    {
        public OrderByEnum OrderByType { get; private set; }

        public CarMakeModelPriceOrderBy()
            : this(OrderByEnum.Ascending)
        {

        }

        public CarMakeModelPriceOrderBy(OrderByEnum orderBy)
        {
            this.OrderByType = orderBy;
        }
    }
}
