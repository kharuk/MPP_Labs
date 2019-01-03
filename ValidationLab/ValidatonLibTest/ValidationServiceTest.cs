using System;
using NUnit.Framework;
using ValidationLib.Services;
using Logger.Interfaces;
using Moq;

namespace ValidatonLibTest
{
    [TestFixture]
    public class ValidationServiceTest
    {
        [Test]
        public void Test_Object_Is_Valid()
        {
            var user = new User("test@gmail.com", "userpassword", 22, "David");
            var service = new ValidationService();
            var actual = service.Validate(user);

            Assert.IsTrue(actual.IsOK);
        }

        [Test]
        public void Test_Object_Is_Not_Valid()
        {
            var user = new User("", "user", 22, "David");
            var service = new ValidationService();
            var actual = service.Validate(user);

            Assert.Greater(actual.ValidationErrors.Count, 0);
        }

        [Test]
        public void Test_Object_is_Null()
            => Assert.Throws<ArgumentNullException>(() => new ValidationService().Validate(null));

        [Test]
        public void Test_That_Logger_Works()
        {
            var loggerMock = new Mock<ILogger>();
            var user = new User("test@gmail.com", "pass", 22, "David");
            var service = new ValidationService(loggerMock.Object);

            loggerMock.Setup(action => action.Warn(It.IsAny<string>()));
            service.Validate(user);

            loggerMock.Verify(action => action.Warn(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Test_Logger_is_Null()
            => Assert.Throws<ArgumentNullException>(() => new ValidationService(null));

        [Test]
        public void Test_Length_Of_Property_Of_String_Type_Less_Than_Minimum_Length()
        {
            var user = new User("test@gmail.com", "pass", 22, "David");
            var service = new ValidationService();
            var error = "The length of the string should be larger than [8].";

            var actual = service.Validate(user);

            Assert.IsFalse(actual.IsOK);
            Assert.Contains(error, actual.ValidationErrors);
        }

        [Test]
        public void Test_Property_Of_Integer_Type_Not_In_Range()
        {
            var user = new User("test@gmail.com", "userpassword", -1, "David");
            var service = new ValidationService();
            var error = "The value should be larger than [1] and lower than [99].";

            var actual = service.Validate(user);

            Assert.IsFalse(actual.IsOK);
            Assert.Contains(error, actual.ValidationErrors);
        }

        [Test]
        public void Test_Required_Property_Is_Null()
        {
            var user = new User("test@gmail.com", "", 22, "David");
            var service = new ValidationService();
            var error = "The value is requied.";

            var actual = service.Validate(user);

            Assert.IsFalse(actual.IsOK);
            Assert.Contains(error, actual.ValidationErrors);
        }

        [Test]
        public void Test_Name_Is_Not_Required_Property()
        {
            var user = new User("test@gmail.com", "userpassword", 22, "David");
            user.Name = null;
            var service = new ValidationService();

            var actual = service.Validate(user);

            Assert.IsTrue(actual.IsOK);
        }
    }
}
