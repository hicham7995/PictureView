namespace PictureView
{
    using System.Configuration;
    using System.Data;
    using System.Windows;

    using PictureView.Dialogs;
    using PictureView.ViewModels;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<IFileDialogService, FileDialogService>();
            containerRegistry.RegisterScoped<IMainWindowViewModel, MainWindowViewModel>();
            containerRegistry.RegisterScoped<IMainWindow, MainWindow>();
            containerRegistry.RegisterScoped<ExplorerTabViewModel>();
            containerRegistry.RegisterScoped<ImageTabViewModel>();
        }
    }
}
