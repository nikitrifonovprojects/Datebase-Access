using NikiCars.Data.DataConstants;
using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarRegistrationOrderByClause : OrderBy
    {
        private OrderByEnum orderBy;

        public CarRegistrationOrderByClause(OrderByEnum orderBy)
        {
            this.orderBy = orderBy;
        }

        public override string GenerateOrderByClause()
        {
            if (this.orderBy == OrderByEnum.None)
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.FIRST_REGISTRATION_DATE}";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.FIRST_REGISTRATION_DATE} ASC";
            }
            else
            {
                return $"{DatabaseTableNames.CARS}.{CarColumns.FIRST_REGISTRATION_DATE} DESC";
            }
        }
    }
}
