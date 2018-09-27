using Sample.Pages;
using Sample.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ViewModels
{
    class MainPageViewModel: ViewModelBase
    {
        public override Task InitializeAsync(object navigationData)
        {
            return Task.WhenAll(NavigationService.NavigateToAsync<HomePage>());
        }
    }
}
