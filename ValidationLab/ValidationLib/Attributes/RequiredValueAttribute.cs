using System;
using ValidationLib.Attributes.Interfaces;

namespace ValidationLib.Attributes
{
    /// <summary>
    /// This attribute verifies whether the property value is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredValueAttribute: Attribute, IValidationAttribute
    {
        public RequiredValueAttribute() { }
        public string ErrorMessage { get; set; }

        public bool IsValid(object value)
        {
            if ( (value == null) || ( value is string && (string)value == ""))
            {
                ErrorMessage = "The value is requied.";
                return false;
            }

            return true;
        }
    }
}
