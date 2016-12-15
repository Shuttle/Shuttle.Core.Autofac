using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.AutoFac
{
    public class AutoFacComponentResolver : IComponentResolver
    {
        private readonly IContainer _container = null;

        public AutoFacComponentResolver(IContainer container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public object Resolve(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public object Resolve(string name, Type serviceType)
        {
            Guard.AgainstNullOrEmptyString(name, "name");
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.ResolveNamed(name, serviceType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                Type enumerableOfType = typeof(IEnumerable<>).MakeGenericType(serviceType);
                return (object[])_container.ResolveService(new TypedService(enumerableOfType));
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }
    }
}