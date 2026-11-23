namespace PictureViewerTests
{
    using Moq;

    using PictureView;
    using PictureView.Dialogs;
    using PictureView.Services;
    using PictureView.ViewModels;

    public class MainWindowViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Open_File_And_A_New_Tab_When_Menu_Open()
        {
            var mainWindowMock = new Mock<IMainWindow>();
            var dialogServiceMock = new Mock<IFileDialogService>();
            var applicationShutDownService = new Mock<IApplicationShutDownService>();
            dialogServiceMock.Setup(o=> o.OpenFiles(It.IsAny<string>(), It.IsAny<bool>())).Returns(["./ChatGPT Image 9 juil. 2025, 19_34_01.png"]);
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, applicationShutDownService.Object);
            viewmodel.OpenCommand.Execute(null);
            dialogServiceMock.Verify(o=>o.OpenFiles(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            Assert.That(viewmodel.Tabs.Count, Is.EqualTo(1));   
        }

        [Test]
        public void Should_Open_Explorer_Tab_When_Menu_Browse()
        {
            var mainWindowMock = new Mock<IMainWindow>();
            var dialogServiceMock = new Mock<IFileDialogService>();
            var applicationShutDownService = new Mock<IApplicationShutDownService>();
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, applicationShutDownService.Object);
            viewmodel.BrowseCommand.Execute(null);
            Assert.That(viewmodel.Tabs.Count, Is.EqualTo(1));
            Assert.That(viewmodel.SelectedTab, Is.TypeOf<ExplorerTabViewModel>());
        }

        [Test]
        public void Should_ShutDown_Application_When_QuitCommand_Executed()
        {
            var mainWindowMock = new Mock<IMainWindow>();
            var dialogServiceMock = new Mock<IFileDialogService>();
            var applicationShutDownServiceMock = new Mock<IApplicationShutDownService>();
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, applicationShutDownServiceMock.Object);
            viewmodel.QuitCommand.Execute(null);
            applicationShutDownServiceMock.Verify(s => s.ShutDown(), Times.Once, "ShutDown should be called once when QuitCommand is executed.");
        }

        [Test]
        public void Should_Not_Open_Explorer_Tab_If_Already_Exists()
        {
            var mainWindowMock = new Mock<IMainWindow>();
            var dialogServiceMock = new Mock<IFileDialogService>();
            var applicationShutDownService = new Mock<IApplicationShutDownService>();
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, applicationShutDownService.Object);
            viewmodel.BrowseCommand.Execute(null);
            var firstTab = viewmodel.SelectedTab;
            viewmodel.BrowseCommand.Execute(null);
            Assert.That(viewmodel.Tabs.Count, Is.EqualTo(1), "There should only be one Explorer tab.");
            Assert.That(viewmodel.SelectedTab, Is.EqualTo(firstTab), "The selected tab should still be the first Explorer tab.");
        }

        [Test]
        [STAThread]
        public void Should_Close_Selected_Tab()
        {
            var dialogServiceMock = new Mock<IFileDialogService>();
            var shutdownMock = new Mock<IApplicationShutDownService>();
            var imagePath = System.IO.Path.GetFullPath("./ChatGPT Image 9 juil. 2025, 19_34_01.png");
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, shutdownMock.Object);

            viewmodel.OpenCommand.Execute(imagePath);
            viewmodel.CloseCommand.Execute(null);

            Assert.That(viewmodel.Tabs, Is.Empty);
            Assert.That(viewmodel.SelectedTab, Is.Null);
        }

        [Test]
        [STAThread]
        public void Should_Close_All_Tabs()
        {
            var dialogServiceMock = new Mock<IFileDialogService>();
            var shutdownMock = new Mock<IApplicationShutDownService>();
            var imagePath = System.IO.Path.GetFullPath("./ChatGPT Image 9 juil. 2025, 19_34_01.png");
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, shutdownMock.Object);

            viewmodel.OpenCommand.Execute(imagePath);
            viewmodel.BrowseCommand.Execute(null);
            viewmodel.CloseAllCommand.Execute(null);

            Assert.That(viewmodel.Tabs, Is.Empty);
            Assert.That(viewmodel.SelectedTab, Is.Null);
        }

        [Test]
        [STAThread]
        public void Should_Keep_Only_Last_Five_Recent_Files()
        {
            var dialogServiceMock = new Mock<IFileDialogService>();
            var shutdownMock = new Mock<IApplicationShutDownService>();
            var imagePath = System.IO.Path.GetFullPath("./ChatGPT Image 9 juil. 2025, 19_34_01.png");
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, shutdownMock.Object);

            for (int index = 0; index < 6; index++)
            {
                viewmodel.OpenCommand.Execute(imagePath);
            }

            Assert.That(viewmodel.RecentFiles.Count, Is.EqualTo(1));
            Assert.That(viewmodel.RecentFiles[0], Is.EqualTo(imagePath));
        }

        [Test]
        [STAThread]
        public void Should_Save_Selected_Image_With_Save_As()
        {
            var dialogServiceMock = new Mock<IFileDialogService>();
            var shutdownMock = new Mock<IApplicationShutDownService>();
            var imagePath = System.IO.Path.GetFullPath("./ChatGPT Image 9 juil. 2025, 19_34_01.png");
            var destination = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "PictureView-test.png");
            dialogServiceMock.Setup(service => service.SaveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(destination);
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, shutdownMock.Object);

            viewmodel.OpenCommand.Execute(imagePath);
            viewmodel.SaveAsCommand.Execute(null);

            dialogServiceMock.Verify(service => service.SaveFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.That(System.IO.File.Exists(destination), Is.True);
        }

        [Test]
        [STAThread]
        public void Should_Print_Selected_Image()
        {
            var dialogServiceMock = new Mock<IFileDialogService>();
            var shutdownMock = new Mock<IApplicationShutDownService>();
            var imagePath = System.IO.Path.GetFullPath("./ChatGPT Image 9 juil. 2025, 19_34_01.png");
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object, shutdownMock.Object);

            viewmodel.OpenCommand.Execute(imagePath);
            viewmodel.PrintCommand.Execute(null);

            dialogServiceMock.Verify(service => service.PrintImage(It.IsAny<System.Windows.Media.Imaging.BitmapSource>()), Times.Once);
        }
    }
}
