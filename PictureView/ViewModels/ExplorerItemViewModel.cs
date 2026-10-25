namespace PictureView.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;

    public class ExplorerItemViewModel : BindableBase
    {
        private readonly ExplorerTabViewModel parent;
        private bool childrenLoaded;

        public ExplorerItemViewModel(string path, ExplorerTabViewModel parent)
        {
            this.Path = path ?? throw new ArgumentNullException(nameof(path));
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public string Path { get; }

        public string Name => string.IsNullOrEmpty(System.IO.Path.GetFileName(this.Path))
            ? this.Path.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)
            : System.IO.Path.GetFileName(this.Path);

        public ObservableCollection<ExplorerItemViewModel> Children { get; } = new();

        public void OnSelected()
        {
            this.LoadChildren();
            this.parent.LoadFiles(this.Path);
        }

        public void LoadChildren()
        {
            if (this.childrenLoaded)
            {
                return;
            }

            this.childrenLoaded = true;

            try
            {
                foreach (var directory in Directory.EnumerateDirectories(this.Path).OrderBy(directory => directory, StringComparer.OrdinalIgnoreCase))
                {
                    this.Children.Add(new ExplorerItemViewModel(directory, this.parent));
                }
            }
            catch
            {
            }
        }
    }
}
