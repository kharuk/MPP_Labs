namespace NugetTestProject.Classes
{
    public class OnMultiple: IClassToInject
    {
        private readonly ClassToInject _objFir;

        private readonly ClassToInject _objSec;

        public OnMultiple(ClassToInject obj, ClassToInject obj1)
        {
            _objFir = obj;
            _objSec = obj1;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;

            return GetType() == o.GetType();
        }
    }
}
