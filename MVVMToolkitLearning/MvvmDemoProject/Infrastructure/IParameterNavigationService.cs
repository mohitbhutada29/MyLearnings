namespace MvvmDemoProject.Infrastructure
{
    /// <summary>
    /// Navigates with parameters. When you want your VM to navigate with parameters, derive the viewmodel with this
    /// interface.
    /// </summary>
    internal interface IParameterNavigationService
    {
        void ParameterInitialize(params object[] parameters);
    }
}