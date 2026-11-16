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
            Assert.That(templateSelector.SelectTemplate(explorerTabViewModel, new DependencyObject()), Is.EqualTo(templateSelector.ExplorerTemplate));
        }

        [Test]
        public void Should_Select_ImageTemplate_When_Item_Is_ImageTabViewModel()
        {
            var imageTabViewModel = new ImageTabViewModel("./ChatGPT Image 9 juil. 2025, 19_34_01.png");
            var templateSelector = new TabContentTemplateSelector();
            //// TODO finish the test here 
            Assert.That(templateSelector.SelectTemplate(imageTabViewModel, new DependencyObject()), Is.EqualTo(templateSelector.ImageTemplate));
        }

        [Test]
        public void Should_Return_Null_When_Item_Is_Neither_ExplorerNorImageTabViewModel()
        {
            var templateSelector = new TabContentTemplateSelector();
            var result = templateSelector.SelectTemplate(new object(), new DependencyObject());
            Assert.That(result, Is.Null);
        }
    }
}
