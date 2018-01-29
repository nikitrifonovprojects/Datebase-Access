namespace NikiCars.Console.Validation
{
    public interface IValidator 
    {
        EntityValidationResult ValidateEntity<T>(T obj) where T : class;
    }
}
