using System;
using System.Collections.Generic;
using System.Reflection;
using ValidationLib.Services.Helpers;
using ValidationLib.Attributes.Interfaces;
using Logger.Interfaces;
using Logger;

namespace ValidationLib.Services
{
    /// <summary>
    /// This class implements ValidationService interface and realizes
    /// logic of validating.
    /// </summary>
    public class ValidationService: IValidationService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Common class constructor without arguments.
        /// </summary>
        /// <remarks>Logger is created inside constructor.</remarks>
        public ValidationService()
        {
          //  _logger = new ValidationLogger();
        }

        public ValidationResult Validate(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{nameof(value)} can't be equal to null.");
            }

            return ValidateObject(value);
        }

        /// <summary>
        /// This method gets all properties of object and validate them.
        /// </summary>
        /// <param name="value">Validated object</param>
        /// <returns>Returns object of Validaation Result class.</returns>
        private ValidationResult ValidateObject(object value)
        {
            var result = new ValidationResult(true, new List<string>());

            foreach (var property in value.GetType().GetProperties())
            {
                if ((property.CustomAttributes != null) && !ValidateProperty(property, value, result.ValidationErrors))
                {
                    result.IsOK = false;
                }
            }

            return result;
        }

        /// <summary>
        /// This method gets attributes and value of property
        /// and verify that concrete property passes all his validation attribute.
        /// </summary>
        /// <param name="property">Concrete property of the verified object.</param>
        /// <param name="value">Verified object.</param>
        /// <param name="errorMessages">Container for errors.</param>
        /// <returns>Will return true if property is valid and vice versa</returns>
        private bool ValidateProperty(PropertyInfo property, object value, List<string> errorMessages)
        {
            bool isValidValue = true;

            foreach (var attribute in property.GetCustomAttributes(typeof(IValidationAttribute), false))
            {
                if (attribute is IValidationAttribute)
                {
                    ValidateAttribute((IValidationAttribute)attribute, value.GetType().GetProperty(property.Name).GetValue(value, null), errorMessages, ref isValidValue);
                }
            }

            return isValidValue;
        }

        /// <summary>
        /// This method verifies whether property passes validation of concrete attribute.
        /// </summary>
        /// <param name="attribute">Concrete attribute.</param>
        /// <param name="value">Verified object.</param>
        /// <param name="errorMessages">Container for errors.</param>
        /// <param name="isValidValue">Reference to ValidateProperty method return result.</param>
        private void ValidateAttribute(IValidationAttribute attribute, object value, List<string> errorMessages, ref bool isValidValue)
        {
            if (!attribute.IsValid(value))
            {
                errorMessages.Add(attribute.ErrorMessage);
              //  _logger.Warn (attribute.ErrorMessage);

                isValidValue = false;
            }
        }
  }
}
