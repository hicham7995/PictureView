namespace PictureView.ViewModels
{
    using System;
    using System.IO;
    using System.Windows.Input;

    public class ExplorerFileViewModel : BindableBase
    {
        public ExplorerFileViewModel(string path, ICommand openImageCommand)
        {
            this.Path = path ?? throw new ArgumentNullException(nameof(path));
            this.OpenCommand = new RelayCommand(
                _ => openImageCommand?.Execute(this.Path),
                _ => File.Exists(this.Path));
            this.LastModified = File.GetLastWriteTime(this.Path);
            this.Icon = this.GetIcon();
        }

        public string Path { get; }

        public string Name => System.IO.Path.GetFileName(this.Path);

        public DateTime LastModified { get; }

        public string DisplayDate => this.LastModified.ToString("dd/MM/yyyy HH:mm");

        public string Icon { get; }

        public ICommand OpenCommand { get; }

        private string GetIcon()
        {
            var extension = System.IO.Path.GetExtension(this.Path).ToLowerInvariant();

            if (extension is ".png" or ".jpg" or ".jpeg" or ".bmp" or ".gif" or ".tif" or ".tiff" or ".webp" or ".ico")
            {
                return this.Path;
            }

            return "pack://application:,,,/PictureView;component/app-icon.png";
        }
    }
}
