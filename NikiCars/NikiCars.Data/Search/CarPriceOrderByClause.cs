using NikiCars.Data.DataConstants;
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
                return $"{DatabaseTableNames.CARS}.{CarColumns.PRICE}";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.PRICE} ASC";
            }
            else
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.PRICE} DESC";
            }
        }
    }
}
