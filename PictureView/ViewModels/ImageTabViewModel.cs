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

        public string FilePath { get; }

        public BitmapImage Image { get; }

        public RelayCommand CloseCommand { get; }

        public ImageTabViewModel(string filePath, Action<ImageTabViewModel>? close = null)
        {
            this.FilePath = System.IO.Path.GetFullPath(filePath);
            this.Title = System.IO.Path.GetFileName(this.FilePath);
            var bmp = new BitmapImage();
            bmp.BeginInit();
            var uri = new Uri("file:///" + this.FilePath.Replace("\\", "/"));
            bmp.UriSource = uri;
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.EndInit();
            bmp.Freeze();

            this.Image = bmp;
            this.CloseCommand = new RelayCommand(_ => close?.Invoke(this));
        }

        public void Save()
        {
            this.SaveAs(this.FilePath);
        }

        public void SaveAs(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            BitmapEncoder encoder = System.IO.Path.GetExtension(filePath).ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" or ".jpe" or ".jfif" => new JpegBitmapEncoder { QualityLevel = 95 },
                ".png" => new PngBitmapEncoder(),
                ".tif" or ".tiff" => new TiffBitmapEncoder(),
                ".bmp" => new BmpBitmapEncoder(),
                ".gif" => new GifBitmapEncoder(),
                ".ico" => new PngBitmapEncoder(),
                _ => throw new NotSupportedException("The selected image format is not supported.")
            };

            encoder.Frames.Add(BitmapFrame.Create(this.Image));
            using var stream = System.IO.File.Create(filePath);
            encoder.Save(stream);
        }
    }
}
