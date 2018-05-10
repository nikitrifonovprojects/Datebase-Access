using NikiCars.Data.DataConstants;
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
                return $"{DatabaseTableNames.CARS}.{CarColumns.KILOMETERS}";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.KILOMETERS} ASC";
            }
            else
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.KILOMETERS} DESC";
            }
        }
    }
}
