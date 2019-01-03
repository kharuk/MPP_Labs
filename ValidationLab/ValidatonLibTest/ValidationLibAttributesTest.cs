using System;
using NUnit.Framework;
using Moq;
using ValidationLib.Attributes;
using ValidationLib.Attributes.Interfaces;

namespace ValidatonLibTest
{
    class ValidationLibAttributesTest
    {
        [Test]
        public void Test_MinimumStringLengthAttribute_Validate_Strings()
        {
            string valString = "Some string";

            Assert.IsTrue(new MinimumStringLengthAttribute(2).IsValid(valString));
        }

        [Test]
        public void Test_Minimum_Length_Must_Be_Larger_Than_Zero_In_MinimumStringLengthAttribute()
            => Assert.Throws<ArgumentOutOfRangeException>(() => new MinimumStringLengthAttribute(0));


        [Test]
        public void Test_MinimumStringLengthAttribute_Does_Not_Validate_Values_With_Other_Types_Except_String()
        {
            var notString = 12;

            Assert.Throws<ArgumentException>(() => new MinimumStringLengthAttribute(2).IsValid(notString));
        }

        [Test]
        public void Test_MinimumStringLengthAttribute_Does_Not_Validate_Null()
            => Assert.Throws<ArgumentNullException>(() => new MinimumStringLengthAttribute(2).IsValid(null));

        [Test]
        public void Test_RangeOfIntegerAttribute_Validate_Integer_Values()
        {
            var integer = 12;

            Assert.IsTrue(new RangeOfIntegerAttribute(2, 18).IsValid(integer));
        }

        [Test]
        public void Test_RangeOfIntegerAttribute_Does_Not_Validate_Values_With_Other_Types_Except_Integer()
        {
            string notInteger = "Some string";

            Assert.Throws<ArgumentException>(() => new RangeOfIntegerAttribute(2, 18).IsValid(notInteger));
        }

        [Test]
        public void Test_The_Lower_Limit_Must_Be_Less_Than_Upper_Limit_In_RangeOfIntegerAttribute()
            => Assert.Throws<ArgumentException>(() => new RangeOfIntegerAttribute(18, 2));

        [Test]
        public void Test_RangeOfIntegerAttribute_Does_Not_Validate_Null_Value()
            => Assert.Throws<ArgumentNullException>(() => new RangeOfIntegerAttribute(2, 18).IsValid(null));
  }
}
