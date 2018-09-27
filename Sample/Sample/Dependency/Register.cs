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
        private void Register()
        {
            _containerBuilder = new ContainerBuilder();

            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>();

            _containerBuilder.RegisterType<MainPageViewModel>();
            _containerBuilder.RegisterType<HomePageViewModel>();
            _containerBuilder.RegisterType<HelpPageViewModel>();
        }
    }
}
