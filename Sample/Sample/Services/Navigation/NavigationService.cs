using Sample.Dependency;
using Sample.Pages;
using Sample.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<MainPage>();
        }

        public Task NavigateToAsync<TPage>() where TPage : Page
        {
            return InternalNavigateToAsync(typeof(TPage), null);
        }

        public Task NavigateToAsync<TPage>(object parameter) where TPage : Page
        {
            return InternalNavigateToAsync(typeof(TPage), parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainPage)
            {
                var mainPage = CurrentApplication.MainPage as MainPage;
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as MainPage;

            if (mainPage != null)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type pageType, object parameter)
        {
            Page page = CreateAndBindPage(pageType, parameter);

            if (page is MainPage)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is MainPage)
            {
                var mainPage = CurrentApplication.MainPage as MainPage;
                var navigationPage = mainPage.Detail as CustomNavigationPage;

                if (navigationPage != null)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if (currentPage.GetType() != page.GetType())
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Page CreateAndBindPage(Type pageType, object parameter)
        {
            Type viewModelType = GetViewModelTypeForPage(pageType);

            if (viewModelType == null)
            {
                throw new Exception($"Mapping type for {pageType} is not a view model");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        protected Type GetViewModelTypeForPage(Type pageType)
        {
            if (!_mappings.ContainsKey(pageType))
            {
                throw new KeyNotFoundException($"No map for ${pageType} was found on navigation mappings");
            }

            return _mappings[pageType];
        }

    }
}
