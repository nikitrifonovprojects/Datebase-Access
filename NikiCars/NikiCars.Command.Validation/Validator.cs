namespace NikiCars.Command.Validation
{
    public class Validator : IValidator
    {
        public EntityValidationResult ValidateEntity<T>(T obj) where T : class
        {
            return new EntityValidator<T>().Validate(obj);
        }
    }
}
