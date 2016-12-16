using Autofac;
using NUnit.Framework;
using Shuttle.Core.AutoFac;
using Shuttle.Core.ComponentContainer.Tests;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Castle.Tests
{
    [TestFixture]
    public class AutoFacComponentContainerFixture : ComponentContainerFixture
    {
        [Test]
        public void Should_be_able_to_register_and_resolve_a_singleton()
        {
            var containerBuilder = new ContainerBuilder();

            var registry = new AutoFacComponentRegistry(containerBuilder);

            RegisterSingleton(registry);

            var resolver = new AutoFacComponentResolver(containerBuilder.Build());

            ResolveSingleton(resolver);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_transient_components()
        {
            var containerBuilder = new ContainerBuilder();

            var registry = new AutoFacComponentRegistry(containerBuilder);

            RegisterTransient(registry);

            var resolver = new AutoFacComponentResolver(containerBuilder.Build());

            ResolveTransient(resolver);
        }


        [Test]
        public void Should_be_able_resolve_all_instances()
        {
            var containerBuilder = new ContainerBuilder();

            var registry = new AutoFacComponentRegistry(containerBuilder);

            RegisterCollection(registry);

            var resolver = new AutoFacComponentResolver(containerBuilder.Build());

            ResolveCollection(resolver);
        }
    }
}