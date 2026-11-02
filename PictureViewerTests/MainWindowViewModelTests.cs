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
    }
}
