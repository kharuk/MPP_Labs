using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationLib.Services.Helpers;

namespace ValidationLib.Services
{
    /// <summary>
    /// This interface must be implemented by ValidationService class.
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// This method validate object or rather its properties with validation attributes.
        /// </summary>
        /// <param name="value">Object to validate</param>
        /// <returns>Object of class ValidationResult, that reflects validation success.</returns>
        ValidationResult Validate(object value);
    }
}
