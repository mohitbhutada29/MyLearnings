namespace MvvmDemoProject.Infrastructure
{
    /// <summary>
    /// If parameters are fetched from database and operation takes time, we can use this service to wait till data
    /// loads in view.
    /// </summary>
    public interface IAsyncNavigationService
    {
        Task InitialzeAsync();
    }
}
