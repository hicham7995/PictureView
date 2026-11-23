namespace PictureView.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using PictureView.Dialogs;
    using PictureView.Services;

    public class MainWindowViewModel : BindableBase, IMainWindowViewModel
    {
        public ObservableCollection<object> Tabs { get; } = new ();

        public ICommand OpenCommand { get; } = null!;

        public ObservableCollection<string> RecentFiles { get; } = new();

        public RelayCommand CloseCommand { get; }

        public RelayCommand CloseAllCommand { get; }

        public RelayCommand SaveCommand { get; }

        public RelayCommand SaveAsCommand { get; }

        public RelayCommand PrintCommand { get; }

        public RelayCommand RecentFileCommand { get; }

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

        public MainWindowViewModel(IFileDialogService fileDialogService, IApplicationShutDownService applicationShutDownService)
        {
            this.fileDialogService = fileDialogService;
            this.OpenCommand = new RelayCommand(parameter =>
            {
                if (parameter is string path)
                {
                    this.OpenFiles(new[] { path });
                }
                else
                {
                    this.OpenFiles();
                }
            });
            this.BrowseCommand = new RelayCommand(_ => this.ShowExplorerTab());
            this.QuitCommand = new RelayCommand(_ => applicationShutDownService.ShutDown());
            this.CloseCommand = new RelayCommand(parameter =>
            {
                if (parameter is null)
                {
                    this.CloseSelectedTab();
                }
                else
                {
                    this.CloseTab(parameter);
                }
            });
            this.CloseAllCommand = new RelayCommand(_ => this.CloseAllTabs());
            this.SaveCommand = new RelayCommand(_ => this.SaveSelectedTab(), _ => this.SelectedTab is ImageTabViewModel);
            this.SaveAsCommand = new RelayCommand(_ => this.SaveSelectedTabAs(), _ => this.SelectedTab is ImageTabViewModel);
            this.PrintCommand = new RelayCommand(_ => this.PrintSelectedTab(), _ => this.SelectedTab is ImageTabViewModel);
            this.RecentFileCommand = new RelayCommand(parameter =>
            {
                if (parameter is string path)
                {
                    this.OpenFiles(new[] { path });
                }
            });
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
            this.OpenFiles(this.fileDialogService.OpenFiles(filter, true));
        }

        private void OpenFiles(string[] files)
        {
            foreach (string file in files.Where(File.Exists))
            {
                try
                {
                    string fullPath = Path.GetFullPath(file);
                    var tab = new ImageTabViewModel(fullPath, imageTab => this.CloseTab(imageTab));
                    this.Tabs.Add(tab);
                    this.SelectedTab = tab;
                    this.AddRecentFile(fullPath);
                }
                catch
                {
                    // Ignore files that cannot be loaded as images.
                }
            }
        }

        private void AddRecentFile(string filePath)
        {
            this.RecentFiles.Remove(filePath);
            this.RecentFiles.Insert(0, filePath);

            while (this.RecentFiles.Count > 5)
            {
                this.RecentFiles.RemoveAt(this.RecentFiles.Count - 1);
            }
        }

        private void CloseSelectedTab()
        {
            if (this.SelectedTab is not null)
            {
                this.CloseTab(this.SelectedTab);
            }
        }

        private void CloseAllTabs()
        {
            this.Tabs.Clear();
            this.SelectedTab = null;
        }

        private void CloseTab(object tab)
        {
            int index = this.Tabs.IndexOf(tab);
            if (index < 0)
            {
                return;
            }

            this.Tabs.Remove(tab);
            if (this.SelectedTab == tab)
            {
                this.SelectedTab = this.Tabs.Count > 0
                    ? this.Tabs[Math.Min(index, this.Tabs.Count - 1)]
                    : null;
            }
        }

        private void SaveSelectedTab()
        {
            if (this.SelectedTab is ImageTabViewModel imageTab)
            {
                imageTab.Save();
            }
        }

        private void SaveSelectedTabAs()
        {
            if (this.SelectedTab is not ImageTabViewModel imageTab)
            {
                return;
            }

            string initialDirectory = this.lastSaveDirectory;
            if (string.IsNullOrWhiteSpace(initialDirectory))
            {
                initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            const string Filter = "PNG|*.png|JPG-JPEG|*.jpg|JPEG|*.jpeg|TIF-TIFF Rev 6|*.tif;*.tiff|BMP-Windows Bitmap RGB 565|*.bmp|ICO-Windows Icon|*.ico|GIF|*.gif|WebP|*.webp|AVIF|*.avif";
            string filePath = this.fileDialogService.SaveFile(Filter, initialDirectory);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            imageTab.SaveAs(filePath);
            this.lastSaveDirectory = Path.GetDirectoryName(filePath) ?? initialDirectory;
        }

        private void PrintSelectedTab()
        {
            if (this.SelectedTab is ImageTabViewModel imageTab)
            {
                this.fileDialogService.PrintImage(imageTab.Image);
            }
        }

        private string lastSaveDirectory = string.Empty;
    }
}
