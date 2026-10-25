namespace PictureView.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;

    public class ExplorerTabViewModel : BindableBase
    {
        private string currentFolder;

        public ExplorerTabViewModel(ICommand openImageCommand)
        {
            this.OpenImageCommand = openImageCommand;
            this.GoToFolderCommand = new RelayCommand(_ => this.NavigateToCurrentPath());

            this.currentFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.BuildExplorerTree();
            this.LoadFiles(this.currentFolder);
        }

        public ObservableCollection<ExplorerItemViewModel> ExplorerTree { get; } = new();

        public ObservableCollection<ExplorerFileViewModel> ExplorerFiles { get; } = new();

        public ICommand OpenImageCommand { get; }

        public RelayCommand GoToFolderCommand { get; }

        public string CurrentFolder
        {
            get => this.currentFolder;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                var fullPath = Path.GetFullPath(value);
                if (Directory.Exists(fullPath))
                {
                    this.LoadFiles(fullPath);
                }
            }
        }

        public void NavigateToCurrentPath()
        {
            if (string.IsNullOrWhiteSpace(this.currentFolder))
            {
                return;
            }

            var fullPath = Path.GetFullPath(this.currentFolder);
            if (Directory.Exists(fullPath))
            {
                this.LoadFiles(fullPath);
            }
        }

        public void OpenFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                this.OpenImageCommand.Execute(filePath);
            }
        }

        public void LoadFiles(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
            {
                return;
            }

            var fullPath = Path.GetFullPath(folder);
            this.SetProperty(ref this.currentFolder, fullPath);
            this.ExplorerFiles.Clear();

            foreach (var file in Directory.EnumerateFiles(fullPath).OrderBy(path => Path.GetFileName(path), StringComparer.OrdinalIgnoreCase))
            {
                this.ExplorerFiles.Add(new ExplorerFileViewModel(file, this.OpenImageCommand));
            }
        }

        private void BuildExplorerTree()
        {
            this.ExplorerTree.Clear();

            foreach (var drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                this.ExplorerTree.Add(new ExplorerItemViewModel(drive.RootDirectory.FullName, this));
            }

            if (!this.ExplorerTree.Any())
            {
                var fallbackPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (Directory.Exists(fallbackPath))
                {
                    this.ExplorerTree.Add(new ExplorerItemViewModel(fallbackPath, this));
                }
            }
        }
    }
}
