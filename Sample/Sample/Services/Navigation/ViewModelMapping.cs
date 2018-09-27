using Sample.Pages;
using Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(MainPage), typeof(MainPageViewModel));
            _mappings.Add(typeof(HomePage), typeof(HomePageViewModel));
            _mappings.Add(typeof(HelpPage), typeof(HelpPageViewModel));
        }
    }
}
