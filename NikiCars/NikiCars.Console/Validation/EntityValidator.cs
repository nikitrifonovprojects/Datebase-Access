using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NikiCars.Console.Validation
{
    public class EntityValidator<T> where T : class
    {
        public EntityValidationResult Validate(T obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj, null, null);
            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return new EntityValidationResult(validationResults);
        }
    }
}
