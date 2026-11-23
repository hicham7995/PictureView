namespace PictureView.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileDialogService : IFileDialogService
    {
        public string[] OpenFiles(string filter, bool multiSelect)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = filter,
                Multiselect = multiSelect
            };

            return dlg.ShowDialog() == true ? dlg.FileNames : Array.Empty<string>();
        }

        public string SaveFile(string filter, string initialDirectory)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                Filter = filter,
                InitialDirectory = initialDirectory
            };

            return dlg.ShowDialog() == true ? dlg.FileName : string.Empty;
        }

        public bool PrintImage(System.Windows.Media.Imaging.BitmapSource image)
        {
            var dialog = new System.Windows.Controls.PrintDialog();
            if (dialog.ShowDialog() != true)
            {
                return false;
            }

            var visual = new System.Windows.Controls.Image
            {
                Source = image,
                Stretch = System.Windows.Media.Stretch.Uniform
            };

            dialog.PrintVisual(visual, "PictureView image print");
            return true;
        }
    }
}
