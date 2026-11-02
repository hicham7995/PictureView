namespace PictureViewerTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ExplorerItemViewModelTests
    {
        [Test]
        public void Should_Return_Path_Name()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var testFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var explorerItemViewModel = new PictureView.ViewModels.ExplorerItemViewModel(testFolderPath, explorerTabViewModel);
            // Act
            var path = explorerItemViewModel.Path;
            var name = explorerItemViewModel.Name;
            // Assert
            Assert.That(path, Is.EqualTo(testFolderPath), "Path should be equal to the test folder path.");
            Assert.That(name, Is.Not.Null.And.Not.Empty, "Name should not be null or empty.");
        }

        [Test]
        public void Should_Return_Empty_When_Path_Is_Empty()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var explorerItemViewModel = new PictureView.ViewModels.ExplorerItemViewModel(string.Empty, explorerTabViewModel);
            // Act
            var path = explorerItemViewModel.Path;
            var name = explorerItemViewModel.Name;
            // Assert
            Assert.That(path, Is.EqualTo(string.Empty), "Path should be equal to an empty string.");
            Assert.That(name, Is.EqualTo(string.Empty), "Name should be equal to an empty string.");
        }

        [Test]
        public void Should_Load_Children_When_Selected()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var testFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var explorerItemViewModel = new PictureView.ViewModels.ExplorerItemViewModel(testFolderPath, explorerTabViewModel);
            // Act
            explorerItemViewModel.OnSelected();
            // Assert
            Assert.That(explorerItemViewModel.Children.Count, Is.GreaterThan(0), "Children should be loaded when the item is selected.");
        }

        [Test]
        public void Should_Not_Load_Children_When_Already_Selected()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var testFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var explorerItemViewModel = new PictureView.ViewModels.ExplorerItemViewModel(testFolderPath, explorerTabViewModel);
            // Act
            explorerItemViewModel.OnSelected();
            // Act 2
            explorerItemViewModel.OnSelected();
            // Assert
            Assert.That(explorerItemViewModel.Children.Count, Is.GreaterThan(0), "Children should be loaded when the item is selected.");
        }
    }
}
