using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TView>() where TView : Page;

        Task NavigateToAsync<TView>(object parameter) where TView : Page;

        Task NavigateBackAsync();

        Task RemoveLastFromBackStackAsync();
    }
}
