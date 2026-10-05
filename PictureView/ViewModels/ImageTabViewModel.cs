namespace PictureView.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Media.Imaging;

    public class ImageTabViewModel : BindableBase
    {
        public string Title { get; }

        public BitmapImage Image { get; }

        public ImageTabViewModel(string filePath)
        {
            this.Title = System.IO.Path.GetFileName(filePath);
            string fullPath = System.IO.Path.GetFullPath(filePath);
            var bmp = new BitmapImage();
            bmp.BeginInit();
            var uri = new Uri("file:///" + fullPath.Replace("\\", "/"));
            bmp.UriSource = uri;
            bmp.CacheOption = BitmapCacheOption.OnLoad; // libère le fichier après chargement
            bmp.EndInit();
            bmp.Freeze();

            this.Image = bmp;
        }
    }
}
