using MvvmDemoProject.Infrastructure;
using MvvmDemoProject.ViewModels;

namespace MvvmDemoProject.NavigationService
{
    /// <summary>
    /// Navigate methods to move across views.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    public class NavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {
        private readonly Func<TViewModel> _createViewModel;
        private readonly NavigationStore _navigationStore;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(params object[] parameters)
        {
            var viewModel = _createViewModel();
            if (viewModel is IParameterNavigationService paramViewModel)
            {
                paramViewModel.ParameterInitialize(parameters);
            }

            _navigationStore.CurrentViewModel = viewModel;
        }

        public async Task AsyncNavigation(params object[] parameters)
        {
            var viewModel = _createViewModel();

            if (viewModel is IParameterNavigationService paramViewModel)
            {
                paramViewModel.ParameterInitialize(parameters);
            }

            if (viewModel is IAsyncNavigationService asyncViewModel)
            {
                await asyncViewModel.InitialzeAsync();
            }

            _navigationStore.CurrentViewModel = viewModel;
        }
    }
}