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
    }
}
