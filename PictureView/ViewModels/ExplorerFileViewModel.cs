namespace PictureView.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Windows.Input;
    public class ExplorerFileViewModel : BindableBase
    {
        public string Path { get; }

        public string Name => System.IO.Path.GetFileName(this.Path);

        // Commande exposée pour la vue (ouvre ce fichier)
        public ICommand OpenCommand { get; }

        public ExplorerFileViewModel(string path, ICommand openImageCommand)
        {
            this.Path = path ?? throw new ArgumentNullException(nameof(path));

            // wrapper qui appelle la commande parent avec le chemin comme paramètre
            this.OpenCommand = new RelayCommand(
                _ => openImageCommand?.Execute(this.Path),
                _ => File.Exists(this.Path));
        }
    }
}
