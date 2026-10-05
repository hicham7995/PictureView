namespace PictureView.TemplateSelector
{
    using PictureView.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    public class TabContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? ExplorerTemplate { get; set; }
        public DataTemplate? ImageTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is ExplorerTabViewModel)
            {
                return this.ExplorerTemplate;
            }

            if (item is ImageTabViewModel)
            {
                return this.ImageTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
