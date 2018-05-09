using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class DateOrderByClause : OrderBy
    {
        private OrderByEnum orderBy;

        public DateOrderByClause(OrderByEnum orderBy)
        {
            this.orderBy = orderBy;
        }

        public override string GenerateOrderByClause()
        {
            if (this.orderBy == OrderByEnum.None)
            {
                return "Cars.DateCreated";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return "Cars.DateCreated ASC";
            }
            else
            {
                return "Cars.DateCreated DESC";
            }
        }
    }
}
