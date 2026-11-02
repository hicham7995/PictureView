namespace PictureViewerTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;

    using Moq;

    using PictureView.TemplateSelector;
    using PictureView.ViewModels;

    public class TabContentSelectorTemplateTests
    {
        [Test]
        public void Should_Select_ExplorerTemplate_When_Item_Is_ExplorerTabViewModel()
        {
            var openImageCommandMock = new Mock<ICommand>();
            var explorerTabViewModel = new ExplorerTabViewModel(openImageCommandMock.Object);
            var templateSelector = new TabContentTemplateSelector();
            //// TODO finish the test here 
           //// Assert.That(templateSelector.SelectTemplate(explorerTabViewModel, new DependencyObject()), Is.EqualTo(templateSelector.ExplorerTemplate));
        }
    }
}
