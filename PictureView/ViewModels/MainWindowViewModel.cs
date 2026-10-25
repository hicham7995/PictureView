namespace PictureView.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using PictureView.Dialogs;

    public class MainWindowViewModel : BindableBase, IMainWindowViewModel
    {
        public ObservableCollection<object> Tabs { get; } = new ();

        public ICommand OpenCommand { get; } = null!;

        public object? SelectedTab
        {
            get => this.selectedTab;
            set
            {
               this.SetProperty(ref this.selectedTab, value);
            }
        }

        public bool ExplorerVisible { get; set; }

        public string ExplorerRootPath { get; set; } = string.Empty;

        public Visibility LeftPaneVisibility { get; set; }

        public RelayCommand QuitCommand { get; set; }

        public RelayCommand BrowseCommand { get; set; }

        private readonly IFileDialogService fileDialogService;

        private object? selectedTab;

        public MainWindowViewModel(IFileDialogService fileDialogService)
        {
            this.fileDialogService = fileDialogService;
            this.OpenCommand = new RelayCommand(_ => this.OpenFiles());
            this.BrowseCommand = new RelayCommand(_ => this.ShowExplorerTab());
            this.QuitCommand = new RelayCommand(_ => System.Windows.Application.Current.Shutdown());
        }

        private void ShowExplorerTab()
        {
            // Vérifie si un onglet Explorer existe déjà
            foreach (var tab in this.Tabs)
            {
                if (tab is ExplorerTabViewModel)
                {
                    this.SelectedTab = tab;
                    return;
                }
            }

            var explorer = new ExplorerTabViewModel(this.OpenCommand);
            this.Tabs.Add(explorer);
            this.SelectedTab = explorer;
        }


        private void OpenFiles()
        {
            string filter = "Images|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff;*.ico;*.webp|All files|*.*";
            var files = this.fileDialogService.OpenFiles(filter, true);

            foreach (var f in files)
            {
                try
                {
                    var tab = new ImageTabViewModel(f);
                    this.Tabs.Add(tab);
                    this.SelectedTab = tab;
                }
                catch
                {
                    // log / ignorer les fichiers non supportés
                }
            }
        }
    }
}
