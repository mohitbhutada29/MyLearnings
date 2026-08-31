using MvvmDemoProject.ViewModels;

namespace MvvmDemoProject.NavigationService
{
    /// <summary>
    /// Contains the current viewmodel infromation with an event to notify when the VM/View is changed
    /// </summary>
    public class NavigationStore
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
