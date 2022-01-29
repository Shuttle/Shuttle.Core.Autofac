using System;
using System.Collections.Generic;
using Autofac;
using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Autofac
{
    public class AutofacComponentRegistry : ComponentRegistry
    {
        private readonly ContainerBuilder _containerBuilder;

        public AutofacComponentRegistry(ContainerBuilder containerBuilder)
        {
            Guard.AgainstNull(containerBuilder, nameof(containerBuilder));

            _containerBuilder = containerBuilder;

            this.AttemptRegisterInstance<IComponentRegistry>(this);
            this.AttemptRegisterComponentResolverDelegate();
        }

        public override IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));
            Guard.AgainstNull(implementationType, nameof(implementationType));

            base.Register(dependencyType, implementationType, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                    {
                        _containerBuilder.RegisterType(implementationType).As(dependencyType)
                            .InstancePerDependency();

                        break;
                    }
                    default:
                    {
                        _containerBuilder.RegisterType(implementationType).As(dependencyType)
                            .SingleInstance();

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

        public override IComponentRegistry RegisterGeneric(Type dependencyType, Type implementationType,
            Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));
            Guard.AgainstNull(implementationType, nameof(implementationType));

            base.RegisterGeneric(dependencyType, implementationType, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                    {
                        _containerBuilder.RegisterGeneric(implementationType).As(dependencyType)
                            .InstancePerDependency();

                        break;
                    }
                    default:
                    {
                        _containerBuilder.RegisterGeneric(implementationType).As(dependencyType)
                            .SingleInstance();

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

        public override IComponentRegistry RegisterCollection(Type dependencyType,
            IEnumerable<Type> implementationTypes,
            Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));
            Guard.AgainstNull(implementationTypes, nameof(implementationTypes));

            base.RegisterCollection(dependencyType, implementationTypes, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                    {
                        foreach (var type in implementationTypes)
                        {
                            _containerBuilder.RegisterType(type).As(dependencyType)
                                .InstancePerDependency();

                            _containerBuilder.RegisterType(type).As(dependencyType).InstancePerDependency();
                        }

                        break;
                    }
                    default:
                    {
                        foreach (var type in implementationTypes)
                        {
                            _containerBuilder.RegisterType(type).As(dependencyType)
                                .SingleInstance();
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

        public override IComponentRegistry RegisterInstance(Type dependencyType, object instance)
        {
            Guard.AgainstNull(dependencyType, nameof(dependencyType));
            Guard.AgainstNull(instance, nameof(instance));

            base.RegisterInstance(dependencyType, instance);

            try
            {
                _containerBuilder.RegisterInstance(instance).As(dependencyType);
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }
    }
}