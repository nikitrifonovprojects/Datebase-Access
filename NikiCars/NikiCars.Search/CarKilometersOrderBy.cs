using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarKilometersOrderBy : IOrderBy, IEntityOrderBy<Car>
    {
        public OrderByEnum OrderByType { get; private set; }

        public CarKilometersOrderBy()
            : this(OrderByEnum.Ascending)
        {

        }

        public CarKilometersOrderBy(OrderByEnum orderBy)
        {
            this.OrderByType = orderBy;
        }
    }
}
