using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarPriceOrderByClause : OrderBy
    {
        private OrderByEnum orderBy;

        public CarPriceOrderByClause(OrderByEnum orderBy)
        {
            this.orderBy = orderBy;
        }

        public override string GenerateOrderByClause()
        {
            if (this.orderBy == OrderByEnum.None)
            {
                return "Cars.Price";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return "Cars.Price ASC";
            }
            else
            {
                return "Cars.Price DESC";
            }
        }
    }
}
