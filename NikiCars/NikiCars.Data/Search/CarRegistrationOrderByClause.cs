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
                return "Cars.FirstRegistrationDate";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return "Cars.FirstRegistrationDate ASC";
            }
            else
            {
                return "Cars.FirstRegistrationDate DESC";
            }
        }
    }
}
