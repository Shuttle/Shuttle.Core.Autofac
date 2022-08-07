# Shuttle.Core.Autofac

> **Warning**
> This package has been deprecated in favour of [.NET Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).

```
PM> Install-Package Shuttle.Core.Autofac
```

The implementation for Autofac makes use of both a `ComponentRegistry` that implements the `IComponentRegistry` interface as well as an `ComponentResolver` that implements the `IComponentResolver` interface.

``` c#
var builder = new ContainerBuilder();

var registry = new AutofacComponentRegistry(builder);

// register all components

var resolver = new AutofacComponentResolver(builder.Build());
```
