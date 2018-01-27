using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NikiCars.Console.Validation
{
    public class EntityValidationResult
    {
        public IList<ValidationResult> Errors { get; private set; }

        public bool HasEroor
        {
            get
            {
                return this.Errors.Count > 0;
            }
        }

        public EntityValidationResult(IList<ValidationResult> errors = null)
        {
            this.Errors = errors ?? new List<ValidationResult>();
        }
    }
}
