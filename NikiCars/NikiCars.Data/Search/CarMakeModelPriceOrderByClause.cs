using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class CarMakeModelPriceOrderByClause : OrderBy
    {
        private OrderByEnum orderBy;

        public CarMakeModelPriceOrderByClause(OrderByEnum orderBy)
        {
            this.orderBy = orderBy;
        }

        public override string GenerateInnerJoin()
        {
            return "INNER JOIN CarMakes ON Cars.CarMakeID = CarMakes.CarMakeID INNER JOIN CarModels ON Cars.CarModelID = CarModels.CarModelID";
        }

        public override string GenerateOrderByClause()
        {
            if (this.orderBy == OrderByEnum.None)
            {
                return "CarMakes.Name, CarModels.Name, Cars.Price";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return "CarMakes.Name, CarModels.Name, Cars.Price ASC";
            }
            else
            {
                return "CarMakes.Name, CarModels.Name, Cars.Price DESC";
            }
        }
    }
}
