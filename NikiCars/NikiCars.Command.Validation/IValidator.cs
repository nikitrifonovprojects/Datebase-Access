namespace NikiCars.Command.Validation
{
    public interface IValidator 
    {
        EntityValidationResult ValidateEntity<T>(T obj) where T : class;
    }
}
