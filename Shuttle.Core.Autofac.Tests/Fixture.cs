using Autofac;
using NUnit.Framework;
using Shuttle.Core.Container.Tests;

namespace Shuttle.Core.Autofac.Tests
{
	[TestFixture]
	public class Fixture : ContainerFixture
	{
		[Test]
		public void Should_be_able_resolve_all_instances()
		{
			var containerBuilder = new ContainerBuilder();

			var registry = new AutofacComponentRegistry(containerBuilder);

			RegisterCollection(registry);

			var resolver = new AutofacComponentResolver(containerBuilder.Build());

			ResolveCollection(resolver);
		}

		[Test]
		public void Should_be_able_to_register_and_resolve_a_singleton()
		{
			var containerBuilder = new ContainerBuilder();

			var registry = new AutofacComponentRegistry(containerBuilder);

			RegisterSingleton(registry);

			var resolver = new AutofacComponentResolver(containerBuilder.Build());

			ResolveSingleton(resolver);
		}

		[Test]
		public void Should_be_able_to_register_and_resolve_transient_components()
		{
			var containerBuilder = new ContainerBuilder();

			var registry = new AutofacComponentRegistry(containerBuilder);

			RegisterTransient(registry);

			var resolver = new AutofacComponentResolver(containerBuilder.Build());

			ResolveTransient(resolver);
		}

		[Test]
		public void Should_be_able_to_register_and_resolve_an_open_generic_singleton()
		{
			var containerBuilder = new ContainerBuilder();

			var registry = new AutofacComponentRegistry(containerBuilder);

			RegisterSingletonGeneric(registry);

			var resolver = new AutofacComponentResolver(containerBuilder.Build());

			ResolveSingletonGeneric(resolver);
		}

		[Test]
		public void Should_be_able_to_register_and_resolve_transient_open_generic_components()
		{
			var containerBuilder = new ContainerBuilder();

			var registry = new AutofacComponentRegistry(containerBuilder);

			RegisterTransientGeneric(registry);

			var resolver = new AutofacComponentResolver(containerBuilder.Build());

			ResolveTransientGeneric(resolver);
		}

		[Test]
		public void Should_be_able_to_register_and_resolve_a_multiple_singleton()
		{
			var containerBuilder = new ContainerBuilder();

			var registry = new AutofacComponentRegistry(containerBuilder);

			RegisterMultipleSingleton(registry);

			var resolver = new AutofacComponentResolver(containerBuilder.Build());

			ResolveMultipleSingleton(resolver);
		}

		[Test]
		public void Should_be_able_to_register_and_resolve_multiple_transient_components()
		{
			var containerBuilder = new ContainerBuilder();

			var registry = new AutofacComponentRegistry(containerBuilder);

			RegisterMultipleTransient(registry);

			var resolver = new AutofacComponentResolver(containerBuilder.Build());

			ResolveMultipleTransient(resolver);
		}
	}
}