using System;
using ValidationLib.Attributes.Interfaces;

namespace ValidationLib.Attributes
{
    /// <summary>
    /// This attribute verifies whether the property length less than
    /// minimum length transferred to constructor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinimumStringLengthAttribute: Attribute, IValidationAttribute
    {
      public string ErrorMessage { get; set; }

      public int MinimumLength { get; set; }

      public MinimumStringLengthAttribute(int minLength)
      {
          if (minLength <= 0)
          {
              throw new ArgumentOutOfRangeException("Minimum length should be larger than 0.");
          }

          MinimumLength = minLength;
      }

      public bool IsValid(object value)
      {
          if (value == null)
          {
              throw new ArgumentNullException("Argument can't be equal to null.");
          }

          if ( !(value is string) )
          {
              throw new ArgumentException("Argument should be string.");
          }

          if ( ((string)value).Length < MinimumLength )
          {
              ErrorMessage = $"The length of the string should be larger than [{MinimumLength}].";
              return false;
          }

          return true;
      }
    }
}
