namespace InjectionTests.Classes
{
    public class ClassToInject : IClassToInject
    {
        public ClassToInject(int a)
        {

        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;

            return GetType() == o.GetType();
        }
    }
}
