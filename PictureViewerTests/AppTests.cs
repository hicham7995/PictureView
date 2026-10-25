namespace PictureViewerTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;

    public class AppTests
    {
        [Test]
        [STAThread]
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
