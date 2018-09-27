using Sample.Dependency;
using Sample.Pages;
using Sample.Services.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sample
{
    public partial class App : Application
    {
        static App()
        {
            BuildDependencies();
        }

        public App()
        {
            InitializeComponent();

            InitNavigation();
        }

        public static void BuildDependencies()
        {
            /// Do you want to use fake services that DO NOT require real backend or internet connection?
            /// Set to true the value of <see cref="AppSettings.DefaultUseFakes"/> to use fake services, false if you want to use Azure Services.

            Locator.Instance.Build();
        }

        private Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            return navigationService.NavigateToAsync<MainPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
