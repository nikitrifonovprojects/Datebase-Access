using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NikiCars.Command.Validation
{
    public class EntityValidationResult
    {
        public IList<ValidationResult> Errors { get; private set; }

        public bool HasError
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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in this.Errors)
            {
                builder.AppendFormat("The validation failed due to: {0}", item.ToString());
            }

            return builder.ToString();
        }
    }
}
