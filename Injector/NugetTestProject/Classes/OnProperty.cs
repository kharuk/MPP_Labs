namespace NugetTestProject.Classes
{
    public class OnProperty: IClassToInject
    {
        public ClassToInject Obj { get; set; }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;

            return GetType() == o.GetType();
        }
    }
}
