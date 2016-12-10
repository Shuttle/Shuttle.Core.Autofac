using Autofac;
using NUnit.Framework;
using Shuttle.Core.AutoFac;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Castle.Tests
{
    [TestFixture]
    public class AutoFacComponentContainerFixture
    {
        [Test]
        public void Should_be_able_to_register_and_resolve_a_type()
        {
            var serviceType = typeof(IDoSomething);
            var implementationType = typeof(DoSomething);
            var bogusType = typeof(object);
            var containerBuilder = new ContainerBuilder();

            var registry = new AutoFacComponentRegistry(containerBuilder);

            registry.Register(serviceType, implementationType, Lifestyle.Singleton);

            var resolver = new AutoFacComponentResolver(containerBuilder.Build());

            Assert.NotNull(resolver.Resolve(serviceType));
            Assert.AreEqual(implementationType, resolver.Resolve(serviceType).GetType());
            Assert.Throws<TypeResolutionException>(() => resolver.Resolve(bogusType));
        }

        [Test]
        public void Should_be_able_to_use_constructor_injection()
        {
            var serviceType = typeof (IDoSomething);
            var implementationType = typeof (DoSomethingWithDependency);
            var someDependency = new SomeDependency();
            var containerBuilder = new ContainerBuilder();

            var registry = new AutoFacComponentRegistry(containerBuilder);

            registry.Register(serviceType, implementationType, Lifestyle.Singleton);
            registry.Register(typeof(ISomeDependency), someDependency);

            var resolver = new AutoFacComponentResolver(containerBuilder.Build());

            Assert.AreSame(someDependency, resolver.Resolve<IDoSomething>().SomeDependency);
        }
    }
}