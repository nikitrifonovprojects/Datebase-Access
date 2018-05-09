using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class CarRegistrationOrderBy : IOrderBy, IEntityOrderBy<Car>
    {
        public OrderByEnum OrderByType { get; private set; }

        public CarRegistrationOrderBy()
            : this(OrderByEnum.Ascending)
        {

        }

        public CarRegistrationOrderBy(OrderByEnum orderBy)
        {
            this.OrderByType = orderBy;
        }
    }
}
