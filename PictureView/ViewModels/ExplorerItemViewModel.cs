namespace PictureView.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;

    public class ExplorerItemViewModel : BindableBase
    {
        public string Path { get; }

        public string Name => System.IO.Path.GetFileName(this.Path);

        public ObservableCollection<ExplorerItemViewModel> Children { get; } = new ();

        private ExplorerTabViewModel _parent;

        public ExplorerItemViewModel(string path, ExplorerTabViewModel parent)
        {
            this.Path = path;
            this._parent = parent;

            try
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    this.Children.Add(new ExplorerItemViewModel(dir, parent));
                }
            }
            catch
            {
            }
        }

        // Appelé lors de la sélection dans le TreeView
        public void OnSelected()
        {
            this._parent.LoadFiles(this.Path);
        }
    }
}
