namespace PictureViewerTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    using PictureView;

    public class AppTests
    {
        [Test]
        public void Should_Test_App()
        {
            Window shell = null;
            var thread = new System.Threading.Thread(() =>
            {
                // Arrange
                var app = new DummyApp();
                // Act
                shell = app.DummyCreateShell();
            });

            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
            // Assert
            if (shell == null)
            {
                throw new Exception("Shell should not be null");
            }
        }
        
        //[Ignore("UI testing doesn't work")]
        //public void Should_Test_MainWindow()
        //{
        //    Window window = null;
        //    var thread = new System.Threading.Thread(() =>
        //    {
        //        // Arrange
        //        var app = new DummyApp();
        //        // Act
        //        window = app.DummyCreateShell();
        //        window.Show();
        //        var treeView = FindVisualChild<TreeView>(window, "ExplorerTreeView");
        //        Assert.That(treeView, Is.Not.Null, "TreeView should not be null");
        //        var item = FindTreeViewItem(treeView, "MyNodeName");

        //        Assert.That(item, Is.Not.Null);

        //        window.Dispatcher.Invoke(() =>
        //        {
        //            var e = new MouseButtonEventArgs(
        //                Mouse.PrimaryDevice,
        //                Environment.TickCount,
        //                MouseButton.Left)
        //            {
        //                RoutedEvent = UIElement.MouseLeftButtonUpEvent,
        //                Source = item
        //            };

        //            item.RaiseEvent(e);
        //        });

        //    });
        //    thread.SetApartmentState(System.Threading.ApartmentState.STA);
        //    thread.Start();
        //    thread.Join();         
        //}

        //private static TreeViewItem? FindTreeViewItem(TreeView treeView, string headerText)
        //{
        //    foreach (var obj in treeView.Items)
        //    {
        //        var container = treeView.ItemContainerGenerator.ContainerFromItem(obj) as TreeViewItem;
        //        if (container == null)
        //            continue;

        //        if (string.Equals(container.Header?.ToString(), headerText, StringComparison.Ordinal))
        //            return container;

        //        var child = FindTreeViewItem(container, headerText);
        //        if (child != null)
        //            return child;
        //    }

        //    return null;
        //}

        //private static TreeViewItem? FindTreeViewItem(TreeViewItem parent, string headerText)
        //{
        //    foreach (var obj in parent.Items)
        //    {
        //        var container = parent.ItemContainerGenerator.ContainerFromItem(obj) as TreeViewItem;
        //        if (container == null)
        //            continue;

        //        if (string.Equals(container.Header?.ToString(), headerText, StringComparison.Ordinal))
        //            return container;

        //        var child = FindTreeViewItem(container, headerText);
        //        if (child != null)
        //            return child;
        //    }

        //    return null;
        //}

        //private static T? FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        //{
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);

        //        if (child is T element && element.Name == name)
        //            return element;

        //        var found = FindVisualChild<T>(child, name);
        //        if (found != null)
        //            return found;
        //    }

        //    return null;
        //}
    }

    public class DummyApp : PictureView.App
    {
        public Window DummyCreateShell()
        {
            this.Initialize();
            return this.CreateShell();
        }
    }
}
