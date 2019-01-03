using System;
using NUnit.Framework;
using InjectionTests.Classes;
using System.Collections.Generic;

namespace Injector.Tests
{
    [TestFixture]
    public class InjectorServiceTests
    {
        [Test]
        public void Test_That_Container_Not_Resolved_Class_Without_Public_Constructor()
        {
            IInjectorService injector = new InjectorService();
            injector.Register<IClassToInject, ClassWithoutPublConstr>();

            Assert.Throws<ArgumentException>(() => { injector.Resolve<IClassToInject>(); });
        }

        [Test]
        public void Test_That_Dependency_Injection_Works_On_Property()
        {
            IInjectorService injector = new InjectorService();
            injector.Register<IClassToInject, OnProperty>();
            injector.Register<IClassToInject, ClassToInject>();

            IList<IClassToInject> actual = injector.Resolve<IClassToInject>();

            OnProperty createdObj = new OnProperty
            {
                Obj = new ClassToInject(1)
            };

            IList<IClassToInject> expected = new List<IClassToInject>
            {
                createdObj,
                createdObj.Obj
            };

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

        [Test]
        public void Test_That_Dependency_Injection_Works_On_Constructor()
        {
            IInjectorService injector = new InjectorService();
            injector.Register<IClassToInject, OnConstructor>();
            injector.Register<IClassToInject, ClassToInject>();

            IList<IClassToInject> actual = injector.Resolve<IClassToInject>();
            ClassToInject createdArg = new ClassToInject(1);
            IClassToInject created = new OnConstructor(createdArg);

            IList<IClassToInject> expected = new List<IClassToInject>
            {
                created,
                createdArg
            };

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

        [Test]
        public void Test_That_Dependency_Injection_Works_With_Multiple()
        {
            IInjectorService injector = new InjectorService();
            injector.Register<IClassToInject, OnMultiple>();
            injector.Register<IClassToInject, ClassToInject>();

            IList<IClassToInject> actual = injector.Resolve<IClassToInject>();
            ClassToInject arg = new ClassToInject(1);
            IClassToInject created = new OnMultiple(arg, arg);

            IList<IClassToInject> expected = new List<IClassToInject>
            {
                created,
                arg
            };

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

        [Test]
        public void Test_That_Dependency_Injection_Works_On_Nested()
        {
            IInjectorService injector = new InjectorService();
            injector.Register<IClassToInject, OnNested>();
            injector.Register<IClassToInject, OnMultiple>();
            injector.Register<IClassToInject, ClassToInject>();

            IList<IClassToInject> actual = injector.Resolve<IClassToInject>();

            ClassToInject arg = new ClassToInject(1);
            OnMultiple createdNested = new OnMultiple(arg, arg);
            IClassToInject created = new OnNested(createdNested);

            IList<IClassToInject> expected = new List<IClassToInject>
            {
                created,
                createdNested,
                arg
            };

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }

    }
}
