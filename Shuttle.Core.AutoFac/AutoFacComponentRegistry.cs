﻿using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core.Registration;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.AutoFac
{
    public class AutoFacComponentRegistry : IComponentRegistry
    {
        private readonly ContainerBuilder _containerBuilder;

        public AutoFacComponentRegistry(ContainerBuilder containerBuilder)
        {
            Guard.AgainstNull(containerBuilder, "containerBuilder");

            _containerBuilder = containerBuilder;
        }

        public IComponentRegistry Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationType, "implementationType");

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                        {
                            _containerBuilder.RegisterType(implementationType).As(serviceType).InstancePerDependency();

                            break;
                        }
                    default:
                        {
                            _containerBuilder.RegisterType(implementationType).As(serviceType).SingleInstance();

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }

        public IComponentRegistry RegisterCollection(Type serviceType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationTypes, "implementationType");

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                        {
                            foreach (var type in implementationTypes)
                            {
                                _containerBuilder.RegisterType(type).As(serviceType).InstancePerDependency();
                            }

                            break;
                        }
                    default:
                        {
                            foreach (var type in implementationTypes)
                            {
                                _containerBuilder.RegisterType(type).As(serviceType).SingleInstance();
                            }

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }

        public IComponentRegistry Register(Type serviceType, object instance)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(instance, "instance");

            try
            {
                _containerBuilder.RegisterInstance(instance).As(serviceType);
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }
    }
}