namespace NugetTestProject.Classes
{
    class OnNested: IClassToInject
    {
        private readonly OnMultiple _obj;

        public OnNested(OnMultiple obj)
        {
            _obj = obj;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;

            return GetType() == o.GetType();
        }
    }
}
