namespace NikiCars.Search.Interfaces
{
    public interface ISearch
    {
        object Value { get; }

        SearchTypeEnum SearchType { get; }
    }
}
