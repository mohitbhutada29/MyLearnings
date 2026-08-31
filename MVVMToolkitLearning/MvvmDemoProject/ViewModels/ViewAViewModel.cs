using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MvvmDemoProject.NavigationService;

namespace MvvmDemoProject.ViewModels
{
    public class ViewAViewModel : BaseViewModel
    {
        private ViewModelRouter _viewModelRouter;

        public ViewAViewModel()
        {
            _viewModelRouter = App.Current.Services.GetService<ViewModelRouter>()!;
            GoToViewB = new RelayCommand(OnGoToViewB);
        }

        private void OnGoToViewB()
        {
            _viewModelRouter.NavigateTo("ViewB");
        }

        public RelayCommand GoToViewB { get; set; }
    }
}
