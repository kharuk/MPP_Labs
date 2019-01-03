using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Injector
{
    /// <summary>
    /// This class implements logic of dependency injection container
    /// </summary>
    public class InjectorService : IInjectorService
    {
        public static InjectorService instance = new InjectorService();
        private IDictionary<Type, IList<TypeModel>> dependencies;

        public InjectorService()
        {
            dependencies = new Dictionary<Type, IList<TypeModel>>();
        }

        public void Register<TContract, TImpl>(TimeToLive timeToLive = TimeToLive.InstancePerDependency)
            where TContract : class
            where TImpl : TContract
        {
            if (!dependencies.ContainsKey(typeof(TContract)))
                dependencies.Add(typeof(TContract), new List<TypeModel>());

            //impl is alredy exist
            if (dependencies[typeof(TContract)].Any(elem => elem.Type == typeof(TImpl)))
                throw new ArgumentException("This implementation is already exist");

            dependencies[typeof(TContract)].Add(new TypeModel(typeof(TImpl), timeToLive));
        }

        public IList<TInterface> Resolve<TInterface>()
        {
            if (!dependencies.ContainsKey(typeof(TInterface)))
                throw new ArgumentException("Unregistered");

            IList<TInterface> listImpl = new List<TInterface>();

            foreach (var impl in dependencies[typeof(TInterface)])
            {
                switch (impl.TimeToLive)
                {
                    case TimeToLive.Singleton:

                        if (impl.Instance == null)
                            impl.Instance = CreateImpl(impl.Type);

                        listImpl.Add((TInterface)impl.Instance);
                        break;

                    case TimeToLive.InstancePerDependency:

                        listImpl.Add((TInterface)CreateImpl(impl.Type));
                        break;
                }
            }

            return listImpl;
        }


        private object CreateImpl(Type impl)
        {
            ConstructorInfo constructor = impl
                .GetConstructors()
                .ToList()
                .OrderByDescending(elem => elem.GetParameters().Count())
                .FirstOrDefault() ?? throw new ArgumentException("There is no public constructor.");

            ParameterInfo[] constructorParameters = constructor.GetParameters();

            object[] objectParameters = new object[constructorParameters.Length];

            int counter = 0;
            foreach (ParameterInfo parameter in constructorParameters)
            {
                if (parameter.ParameterType.IsInterface)
                {
                    if (!dependencies.ContainsKey(parameter.ParameterType))
                        throw new ArgumentException("Unregistered");

                    objectParameters[counter] = CreateImpl(dependencies[parameter.ParameterType].First().Type);
                }
                else
                    objectParameters[counter] = parameter.ParameterType.IsValueType ? Activator.CreateInstance(parameter.ParameterType) : CreateImpl(parameter.ParameterType);

                counter++;
            }

            object objImpl = constructor.Invoke(objectParameters);

            InjectProps(objImpl);

            return objImpl;
        }

        private void InjectProps(object objImpl)
        {
            PropertyInfo[] props = objImpl.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                object propValue;
                if (prop.PropertyType.IsInterface)
                {
                    if (!dependencies.ContainsKey(prop.PropertyType))
                        throw new ArgumentException("Unregistered");

                    propValue = CreateImpl(dependencies[prop.PropertyType].First().Type);
                }
                else
                    propValue = prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : CreateImpl(prop.PropertyType);

                prop.SetValue(objImpl, propValue);
            }
        }
    }
}
