using Autofac;
using Sample.Services.Navigation;
using Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Dependency
{
    public partial class Locator
    {
        private IContainer _container;
        private ContainerBuilder _containerBuilder;

        private static readonly Locator _instance = new Locator();

        public static Locator Instance
        {
            get
            {
                return _instance;
            }
        }

        public Locator()
        {
            Register();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void Register<T>() where T : class
        {
            _containerBuilder.RegisterType<T>();
        }

        public void Build()
        {
            _container = _containerBuilder.Build();
        }
    }
}
