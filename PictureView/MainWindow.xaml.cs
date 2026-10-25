namespace PictureView
{
    using PictureView.ViewModels;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow(IMainWindowViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        private void TreeViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement element)
            {
                return;
            }

            var item = element.DataContext as ExplorerItemViewModel;
            item?.OnSelected();
        }

        private void FileItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView)
            {
                var selected = listView.SelectedItem as ExplorerFileViewModel;
                selected?.OpenCommand.Execute(null);
                return;
            }

            if (sender is FrameworkElement element)
            {
                var file = element.DataContext as ExplorerFileViewModel;
                file?.OpenCommand.Execute(null);
            }
        }
    }
}
