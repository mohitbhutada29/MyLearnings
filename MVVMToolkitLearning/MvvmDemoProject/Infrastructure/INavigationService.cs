namespace MvvmDemoProject.Infrastructure
{
    /// <summary>
    /// Has navigate methods.
    /// </summary>
    public interface INavigationService
    {
        void Navigate(params object[] parameters);

        Task AsyncNavigation(params object[] parameters);
    }
}