using System.Collections.Generic;

namespace ValidationLib.Services.Helpers
{
    /// <summary>
    /// Objects of this class returns as result of validating in validation service.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// This property reflects the success of validation.
        /// </summary>
        /// <value>This property get/set true or false.</value>
        public bool IsOK { get; set; }

        /// <summary>
        /// This property is list of errors Resulting from validation.
        /// </summary>
        /// <remarks>This property will be empty, if validation is successful and vice versa.</remarks>
        public List<string> ValidationErrors { get; set; }

        /// <summary>
        /// Constructor of Validation Result
        /// </summary>
        /// <param name="result">Set value for IsOK property.</param>
        /// <param name="errorMessages">Set value for ValidationErrors property.</param>
        public ValidationResult(bool result, List<string> errorMessages)
        {
            IsOK = result;
            ValidationErrors = errorMessages;
        }
  }
}
