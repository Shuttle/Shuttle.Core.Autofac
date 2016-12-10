# Shuttle.Core.AutoFac

# AutoFac

The implementation for AutoFac makes use of both an `AutoFacComponentRegistry` that implements the `IComponentRegistry` interface as well as an 'AutoFacComponentResolver` that implements the `IComponentResolver` interface.

~~~c#
var builder = new ContainerBuilder();

var registry = new AutoFacComponentRegistry(builder);

// register all components

var resolver = new AutoFacComponentResolver(builder.Build());
~~~



