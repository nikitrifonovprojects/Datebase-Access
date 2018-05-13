namespace NikiCars.Search.Interfaces
{
    public interface ISearch
    {
        object Value { get; }

        SearchEnum SearchType { get; }
    }
}
