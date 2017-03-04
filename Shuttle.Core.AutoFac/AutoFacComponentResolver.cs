using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Autofac
{
    public class AutofacComponentResolver : IComponentResolver
    {
        private readonly IContainer _container = null;

        public AutofacComponentResolver(IContainer container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public object Resolve(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");

            try
            {
                return _container.Resolve(dependencyType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");

            try
            {
                Type enumerableOfType = typeof(IEnumerable<>).MakeGenericType(dependencyType);
                return (object[])_container.ResolveService(new TypedService(enumerableOfType));
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }
    }
}