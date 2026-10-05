namespace PictureView.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IFileDialogService
    {
        string[] OpenFiles(string filter, bool multiSelect);
    }
}
