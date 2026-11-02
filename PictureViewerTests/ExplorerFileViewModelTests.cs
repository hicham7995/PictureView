namespace PictureViewerTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ExplorerFileViewModelTests
    {
        [Test]
        public void Should_Execute_OpenImageCommand_When_Opened()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerFileViewModel = new PictureView.ViewModels.ExplorerFileViewModel("test.jpg", openImageCommandMock.Object);
            // Act
            explorerFileViewModel.OpenCommand.Execute(null);
            // Assert
            openImageCommandMock.Verify(cmd => cmd.Execute("test.jpg"), Moq.Times.Once, "OpenImageCommand should be executed with the correct file path.");
        }

        [Test]
        public void Should_Return_Correct_Display_Date()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerFileViewModel = new PictureView.ViewModels.ExplorerFileViewModel("test.jpg", openImageCommandMock.Object);
            
            var expectedDisplayDate = explorerFileViewModel.LastModified.ToString("dd/MM/yyyy HH:mm");
            // Act
            var displayDate = explorerFileViewModel.DisplayDate;
            // Assert
            Assert.That(displayDate, Is.EqualTo(expectedDisplayDate), "DisplayDate should return the correct formatted date.");
        }

        [Test]
        public void Should_Name_Be_Equal_To_FileName()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerFileViewModel = new PictureView.ViewModels.ExplorerFileViewModel("C:\\path\\to\\test.jpg", openImageCommandMock.Object);
            // Act
            var name = explorerFileViewModel.Name;
            // Assert
            Assert.That(name, Is.EqualTo("test.jpg"), "Name should return the correct file name extracted from the path.");
        }

        [Test]
        public void Should_Throw_ArgumentNullException_If_Path_Is_Null()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PictureView.ViewModels.ExplorerFileViewModel(null, openImageCommandMock.Object));
        }

        [Test]
        public void Should_CanExecute_OpenCommand_Based_On_File_Existence()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerFileViewModel = new PictureView.ViewModels.ExplorerFileViewModel("test.jpg", openImageCommandMock.Object);
            // Act
            var canExecute = explorerFileViewModel.OpenCommand.CanExecute(null);
            // Assert
            Assert.That(canExecute, Is.EqualTo(System.IO.File.Exists("test.jpg")), "CanExecute should return true if the file exists, false otherwise.");
        }

        [Test]
        [TestCase("test.png")]
        [TestCase("test.jpeg")]
        [TestCase("test.bmp")]
        [TestCase("test.gif")]
        [TestCase("test.tif")]
        [TestCase("test.tiff")]
        [TestCase("test.webp")]
        [TestCase("test.ico")]
        public void Should_Return_Image_Path_For_Supported_Image_Extensions(string fileName)
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerFileViewModel = new PictureView.ViewModels.ExplorerFileViewModel(fileName, openImageCommandMock.Object);
            // Act
            var icon = explorerFileViewModel.Icon;
            // Assert
            Assert.That(icon, Is.EqualTo(fileName), $"Icon should return the file path for {fileName} extension.");
        }

        [Test]
        [TestCase("test.txt")]
        [TestCase("test.docx")]
        [TestCase("test.pdf")]
        public void Should_Return_Default_Icon_For_Unsupported_Extensions(string fileName)
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerFileViewModel = new PictureView.ViewModels.ExplorerFileViewModel(fileName, openImageCommandMock.Object);
            var expectedIcon = "pack://application:,,,/PictureView;component/app-icon.png";
            // Act
            var icon = explorerFileViewModel.Icon;
            // Assert
            Assert.That(icon, Is.EqualTo(expectedIcon), "Icon should return the default icon path for unsupported extensions.");
        }

        [Test]
        public void Should_Return_Image_Path_For_Uppercase_Image_Extension()
        {
            // Arrange
            var openImageCommandMock = new Moq.Mock<System.Windows.Input.ICommand>();
            var explorerFileViewModel = new PictureView.ViewModels.ExplorerFileViewModel("test.JPG", openImageCommandMock.Object);
            // Act
            var icon = explorerFileViewModel.Icon;
            // Assert
            Assert.That(icon, Is.EqualTo("test.JPG"), "Icon should return the file path even for uppercase extensions.");
        }
    }
}
