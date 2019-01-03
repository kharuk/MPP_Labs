using System;
using ValidationLib.Attributes.Interfaces;

namespace ValidationLib.Attributes
{
    /// <summary>
    /// This attribute verifies whether the property value in
    /// range of two limit values transferred to constructor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeOfIntegerAttribute: Attribute, IValidationAttribute
    {
        public string ErrorMessage { get; set; }

        public int MinimumValue { get; set; }

        public int MaximumValue { get; set; }

        public RangeOfIntegerAttribute(int minValue, int maxValue)
        {
            if(minValue >= maxValue)
            {
                throw new ArgumentException($"[{nameof(minValue)}] can't be larger or equal [{nameof(maxValue)}]");
            }

            MinimumValue = minValue;
            MaximumValue = maxValue;
        }

        public bool IsValid(object value)
        {
            if( value == null)
            {
                throw new ArgumentNullException("Argument can't be equal to null.");
            }

            if( !isInteger(value))
            {
                throw new ArgumentException("Argument must have an integer type");
            }

            var integer = (int)value;

            if ((integer < MinimumValue) || (integer > MaximumValue))
            {
                ErrorMessage = $"The value should be larger than [{MinimumValue}] and lower than [{MaximumValue}].";
                return false;
            }

            return true;
        }

        private bool isInteger(object value)
        {
            return ((value is byte) || (value is short) || (value is ushort) ||
                (value is int) || (value is uint) || (value is long) || (value is ulong));
        }
    }
}
