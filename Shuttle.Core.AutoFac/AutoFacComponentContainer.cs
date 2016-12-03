using System;
using Autofac;
using Autofac.Core.Registration;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.AutoFac
{
    public class AutoFacComponentContainer : IComponentContainer
    {
        private IContainer _container = null;

        public AutoFacComponentContainer(IContainer container)
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

        public IComponentContainer Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationType, "implementationType");

            var containerBuilder = new ContainerBuilder();

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Thread:
                        {
                            containerBuilder.RegisterType(implementationType).As(serviceType).InstancePerLifetimeScope();

                            break;
                        }
                    case Lifestyle.Transient:
                        {
                            containerBuilder.RegisterType(implementationType).As(serviceType).InstancePerRequest();

                            break;
                        }
                    default:
                        {
                            containerBuilder.RegisterType(implementationType).As(serviceType).SingleInstance();

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

#pragma warning disable 618
            containerBuilder.Update(_container);
#pragma warning restore 618

            return this;
        }

        public IComponentContainer Register(Type serviceType, object instance)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(instance, "instance");

            var containerBuilder = new ContainerBuilder();

            try
            {
                containerBuilder.RegisterInstance(instance).As(serviceType);
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

#pragma warning disable 618
            containerBuilder.Update(_container);
#pragma warning restore 618

            return this;
        }

        public bool IsRegistered(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            return _container.IsRegistered(serviceType);
        }

        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }
    }
}