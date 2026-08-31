using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MvvmDemoProject.NavigationService;

namespace MvvmDemoProject.ViewModels
{
    public class ViewBViewModel : BaseViewModel
    {
        private ViewModelRouter _viewModelRouter;

        public ViewBViewModel()
        {
            _viewModelRouter = App.Current.Services.GetService<ViewModelRouter>()!;
            GoToViewA = new RelayCommand(OnGoToViewA);
        }

        private void OnGoToViewA()
        {
            _viewModelRouter.NavigateTo("ViewA");
        }

        public RelayCommand GoToViewA { get; set; }
    }
}
