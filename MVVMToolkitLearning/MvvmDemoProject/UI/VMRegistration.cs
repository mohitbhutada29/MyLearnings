using Microsoft.Extensions.DependencyInjection;
using MvvmDemoProject.Infrastructure;
using MvvmDemoProject.NavigationService;
using MvvmDemoProject.ViewModels;

namespace MvvmDemoProject.UI
{
    /// <summary>
    /// Registers all the viewmodels and the vies in the navigation service.
    /// </summary>
    public class VMRegistration
    {
        public static void AddNavigationViewModels(ServiceCollection services)
        {
            services.AddSingleton<ViewAViewModel>();
            services.AddSingleton<ViewBViewModel>();
        }

        public static IDictionary<string, INavigationService> RegisterForNavigation(IServiceProvider serviceProvider)
        {
            Dictionary<string, INavigationService> navigableVMS = new Dictionary<string, INavigationService>
            {
                {
                    "ViewA",
                    new NavigationService<ViewAViewModel>((NavigationStore)serviceProvider.GetService(typeof(NavigationStore)),
                    () => (ViewAViewModel)serviceProvider.GetService(typeof(ViewAViewModel)))
                },
                {
                    "ViewB",
                    new NavigationService<ViewBViewModel>((NavigationStore)serviceProvider.GetService(typeof(NavigationStore)),
                    () => (ViewBViewModel)serviceProvider.GetService(typeof(ViewBViewModel)))
                }
            };
            return navigableVMS;
        }
    }
}