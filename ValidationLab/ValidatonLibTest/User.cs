using ValidationLib.Attributes;

namespace ValidatonLibTest
{
    internal class User
    {
        [RequiredValue]
        public string Email { get; set; }

        [RequiredValue]
        [MinimumStringLength(8)]
        public string Password { get; set; }

        [RangeOfInteger(1, 99)]
        public int Age { get; set; }

        public string Name { get; set; }

        public User(string email, string password, int age, string name)
        {
            Email = email;
            Password = password;
            Age = age;
            Name = name;
        }
    }
}