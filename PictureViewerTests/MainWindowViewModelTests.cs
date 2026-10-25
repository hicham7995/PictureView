namespace PictureViewerTests
{
    using Moq;

    using PictureView;
    using PictureView.Dialogs;
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
            dialogServiceMock.Setup(o=> o.OpenFiles(It.IsAny<string>(), It.IsAny<bool>())).Returns(["./ChatGPT Image 9 juil. 2025, 19_34_01.png"]);
            var viewmodel = new MainWindowViewModel(dialogServiceMock.Object);
            viewmodel.OpenCommand.Execute(null);
            dialogServiceMock.Verify(o=>o.OpenFiles(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            Assert.That(viewmodel.Tabs.Count, Is.EqualTo(1));   
        }        
    }
}
