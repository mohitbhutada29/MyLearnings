using Microsoft.Extensions.DependencyInjection;
using MvvmDemoProject.NavigationService;
using MvvmDemoProject.UI;
using System.Windows;

namespace MvvmDemoProject
{
    //Steps to implement navigation using community toolkit and dependency injection
    //In App.xaml.cs define IServiceProvider Services
    //In the constructor define a method ConfigureServices to initialize Services
    //Register MainWindowViewModel
    //Register NavigationStore. NavigationStore contains CurrentViewModel property of type BaseViewModel
    //and an event  public event Action CurrentViewModelChanged;
    //Create all the views and viewmodels. ViewModels will be derived from BaseViewModel.
    //BaseViewModel is derived from ObservableObject.
    // Create a class VMRegistration to register all the viewmodels in the services container.
    // Create a ViewModelRouter having IDictionary<string, INavigationService> _routes, NavigateTo and AsyncNavigation methods.
    // VMRegistration.RegisterForNavigation(serviceProvider) will create IDictionary<string, INavigationService> which is then
    // passed on to ViewModelRouter
    // A resource dictionary is created to store the data templates which maps Views with viewmodels
    //This data template is then added to the app.xaml's resources as merged dictionary.

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<NavigationStore>();
            VMRegistration.AddNavigationViewModels(services);

            services.AddSingleton(serviceProvider =>
                new ViewModelRouter(VMRegistration.RegisterForNavigation(serviceProvider)));

            return services.BuildServiceProvider();
        }
    }
}