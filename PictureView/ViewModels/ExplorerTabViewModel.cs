namespace PictureView.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Windows.Input;

    public class ExplorerTabViewModel : BindableBase
    {
        public ObservableCollection<ExplorerItemViewModel> ExplorerTree { get; } = new();
        public ObservableCollection<ExplorerFileViewModel> ExplorerFiles { get; } = new();
        public ICommand OpenImageCommand { get; }

        public ExplorerTabViewModel(ICommand openImageCommand)
        {
            this.OpenImageCommand = openImageCommand;

            // Dossier racine par défaut : Images
            var root = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var rootItem = new ExplorerItemViewModel(root, this);
            this.ExplorerTree.Add(rootItem);
            this.LoadFiles(root);
        }

        // Charge les fichiers du dossier sélectionné
        public void LoadFiles(string folder)
        {
            this.ExplorerFiles.Clear();

            try
            {
                foreach (var file in Directory.GetFiles(folder))
                {
                    this.ExplorerFiles.Add(new ExplorerFileViewModel(file, this.OpenImageCommand));
                }
            }
            catch
            {
                /* Gérer les exceptions d'accès si besoin */
            }
        }
    }
}
