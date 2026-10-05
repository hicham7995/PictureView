namespace PictureView.ViewModels
{
    using System.Windows.Input;

    public interface IMainWindowViewModel
    {
        ICommand OpenCommand { get; }

        object? SelectedTab { get; set; }

        bool ExplorerVisible { get; set; }

        string ExplorerRootPath { get; set; }

        RelayCommand QuitCommand { get; set; }

        RelayCommand BrowseCommand { get; set; }
    }
}
