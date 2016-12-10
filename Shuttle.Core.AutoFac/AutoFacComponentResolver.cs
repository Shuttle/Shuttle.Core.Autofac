using System;
using Autofac;
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
    }
}