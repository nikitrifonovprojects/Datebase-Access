namespace NikiCars.Data.Search
{
    internal interface IOrderByClause
    {
        string GenerateOrderByClause();

        string GenerateInnerJoin();
    }
}
