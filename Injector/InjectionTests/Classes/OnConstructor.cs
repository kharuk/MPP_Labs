namespace InjectionTests.Classes
{
    class OnConstructor: IClassToInject
    {

        private readonly ClassToInject _classToInject;

        public OnConstructor(ClassToInject classToInject)
        {
            _classToInject = classToInject;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;

            return GetType() == o.GetType();
        }
    }
}
