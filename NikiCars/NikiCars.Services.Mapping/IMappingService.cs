namespace NikiCars.Services.Mapping
{
    public interface IMappingService
    {
        TOut Map<TOut>(object model);
    }
}
