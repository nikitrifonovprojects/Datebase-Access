using NikiCars.Data.Models;
using NikiCars.Search.Interfaces;

namespace NikiCars.Search
{
    public class DateOrderBy : IOrderBy, IEntityOrderBy<Car>
    {
        public OrderByEnum OrderByType { get; private set; }

        public DateOrderBy()
            : this(OrderByEnum.Ascending)
        {

        }

        public DateOrderBy(OrderByEnum orderBy)
        {
            this.OrderByType = orderBy;
        }
    }
}
