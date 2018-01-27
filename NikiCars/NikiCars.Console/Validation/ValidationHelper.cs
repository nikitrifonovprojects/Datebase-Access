namespace NikiCars.Console.Validation
{
    public class ValidationHelper
    {
        public static EntityValidationResult ValidateEntity<T>(T obj) where T : class
        {
            return new EntityValidator<T>().Validate(obj);
        }
    }
}
