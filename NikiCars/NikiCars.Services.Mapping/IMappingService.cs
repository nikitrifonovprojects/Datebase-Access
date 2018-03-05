namespace NikiCars.Services.Mapping
{
    public interface IMappingService
    {
        TOut Map<TOut>(object model);

        TOut Map<TIn, TOut>(TIn source, TOut destination);
    }
}
