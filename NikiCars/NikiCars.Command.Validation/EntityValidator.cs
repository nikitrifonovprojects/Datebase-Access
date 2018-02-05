using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NikiCars.Command.Validation
{
    public class EntityValidator<T> where T : class
    {
        public EntityValidationResult Validate(T obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj, null, null);
            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return new EntityValidationResult(validationResults);
        }
    }
}
