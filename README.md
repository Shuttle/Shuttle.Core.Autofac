# Shuttle.Core.AutoFac

# AutoFacComponentContainer

The `AutoFacComponentContainer` implements the `IComponentContainer` interface.  

~~~c#
var builder = new ContainerBuilder();

// use builder to register all dependencies

var container = new AutoFacComponentContainer(builder.Build());
~~~

AutoFac separates component registration from the resolution side of things.  However, the `IComponentContainer` interface does both.  This leads to a rather odd implementation using the `Update()` method of the builder.  This interface was marked as obsolete at last glance so this `IComponentContainer` implementation may need to be revisted at a later stage.

