using NikiCars.Data.DataConstants;
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
            return $@"INNER JOIN {DatabaseTableNames.CAR_MAKES} ON {DatabaseTableNames.CARS}.{CarColumns.CARMAKE_ID} = {DatabaseTableNames.CAR_MAKES}.{CarMakesColumns.CAR_MAKE_ID}
                      INNER JOIN {DatabaseTableNames.CAR_MODELS} ON {DatabaseTableNames.CARS}.{CarColumns.CARMODEL_ID} = {DatabaseTableNames.CAR_MODELS}.{CarModelsColumns.CAR_MODEL_ID}";
        }

        public override string GenerateOrderByClause()
        {
            if (this.orderBy == OrderByEnum.None)
            {
                return $"{DatabaseTableNames.CAR_MAKES}.{CarMakesColumns.NAME}, {DatabaseTableNames.CAR_MODELS}.{CarModelsColumns.NAME}, {DatabaseTableNames.CARS}.{CarColumns.PRICE}";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return $"{DatabaseTableNames.CAR_MAKES}.{CarMakesColumns.NAME}, {DatabaseTableNames.CAR_MODELS}.{CarModelsColumns.NAME}, {DatabaseTableNames.CARS}.{CarColumns.PRICE} ASC";
            }
            else
            {
                return $"{DatabaseTableNames.CAR_MAKES}.{CarMakesColumns.NAME}, {DatabaseTableNames.CAR_MODELS}.{CarModelsColumns.NAME}, {DatabaseTableNames.CARS}.{CarColumns.PRICE} DESC";
            }
        }
    }
}
