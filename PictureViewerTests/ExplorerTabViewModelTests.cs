namespace PictureViewerTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ExplorerTabViewModelTests
    {
        [Test]
        public void Should_Load_Files_From_ExplorerTabViewModel()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            // Act
            explorerTabViewModel.LoadFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            // Assert
            Assert.That(explorerTabViewModel.ExplorerFiles.Count, Is.GreaterThan(0), "ExplorerFiles should contain files from the My Pictures folder.");
        }

        [Test]
        public void Should_Keep_Default_Folder_Files_When_Loading_Nonexistent_Folder()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var nonexistentFolderPath = "C:\\This\\Path\\Does\\Not\\Exist";
            // Act
            explorerTabViewModel.LoadFiles(nonexistentFolderPath);
            // Assert
            Assert.That(explorerTabViewModel.ExplorerFiles.Count, Is.GreaterThan(0), "ExplorerFiles should contain files from the My Pictures folder..");
        }

        [Test]
        public void Should_Navigate_To_Current_Path()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var testFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            explorerTabViewModel.CurrentFolder = testFolderPath;
            // Act
            explorerTabViewModel.GoToFolderCommand.Execute(null);
            // Assert
            Assert.That(explorerTabViewModel.CurrentFolder, Is.EqualTo(testFolderPath), "CurrentFolder should be set to the test folder path.");
        }

        [Test]
        public void Should_Not_Navigate_To_Empty_Path()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var initialFolderPath = explorerTabViewModel.CurrentFolder;
            explorerTabViewModel.CurrentFolder = string.Empty;
            // Act
            explorerTabViewModel.GoToFolderCommand.Execute(null);
            // Assert
            Assert.That(explorerTabViewModel.CurrentFolder, Is.EqualTo(initialFolderPath), "CurrentFolder should remain unchanged when navigating to an empty path.");
        }

        [Test]
        public void Should_Not_Navigate_To_Nonexistent_Path()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var initialFolderPath = explorerTabViewModel.CurrentFolder;
            var nonexistentPath = "C:\\This\\Path\\Does\\Not\\Exist";
            explorerTabViewModel.CurrentFolder = nonexistentPath;
            // Act
            explorerTabViewModel.GoToFolderCommand.Execute(null);
            // Assert
            Assert.That(explorerTabViewModel.CurrentFolder, Is.EqualTo(initialFolderPath), "CurrentFolder should remain unchanged when navigating to a nonexistent path.");
        }

        [Test]
        public void Should_Keep_Old_Folder_When_Loading_Empty_Folder()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerTabViewModel = new PictureView.ViewModels.ExplorerTabViewModel(openImageCommandMock.Object);
            var initialFolderPath = explorerTabViewModel.CurrentFolder;
            var nonexistentFolderPath = string.Empty; // Simulating an empty folder path
            // Act
            explorerTabViewModel.LoadFiles(nonexistentFolderPath);
            // Assert
            Assert.That(explorerTabViewModel.CurrentFolder, Is.EqualTo(initialFolderPath), "CurrentFolder should remain unchanged when loading a nonexistent folder.");
        }
    }
}
