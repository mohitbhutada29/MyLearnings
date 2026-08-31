using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using MvvmDemoProject.NavigationService;
using MvvmDemoProject.ViewModels;

namespace MvvmDemoProject
{
    public class MainWindowViewModel : ObservableObject
    {
        private NavigationStore _navigationStore;
        private BaseViewModel _currentViewModel;
        private ViewModelRouter _viewModelRouter;

        public MainWindowViewModel()
        {
            _navigationStore = App.Current.Services.GetService<NavigationStore>()!;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _viewModelRouter = App.Current.Services.GetService<ViewModelRouter>()!;

            //Default view
            _viewModelRouter.NavigateTo("ViewA", App.Current.Services);
        }

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                SetProperty(ref _currentViewModel, value);
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModel = _navigationStore.CurrentViewModel;
        }
    }
}