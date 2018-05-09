namespace NikiCars.Data.Search
{
    public abstract class OrderBy : IOrderByClause
    {
        public virtual string GenerateInnerJoin()
        {
            return string.Empty;
        }

        public abstract string GenerateOrderByClause();
        
    }
}
