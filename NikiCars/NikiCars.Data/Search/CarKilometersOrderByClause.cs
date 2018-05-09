using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarKilometersOrderByClause : OrderBy
    {
        private OrderByEnum orderBy;

        public CarKilometersOrderByClause(OrderByEnum orderBy)
        {
            this.orderBy = orderBy;
        }

        public override string GenerateOrderByClause()
        {
            if (this.orderBy == OrderByEnum.None)
            {
                return "Cars.Kilometers";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return "Cars.Kilometers ASC";
            }
            else
            {
                return "Cars.Kilometers DESC";
            }
        }
    }
}
