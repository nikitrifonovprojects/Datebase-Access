using NikiCars.Data.DataConstants;
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
                return $"{DatabaseTableNames.CARS}.{CarColumns.DATE_CREATED}";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.DATE_CREATED} ASC";
            }
            else
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.DATE_CREATED} DESC";
            }
        }
    }
}
