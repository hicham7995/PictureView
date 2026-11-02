namespace PictureView.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class ApplicationShutDownService : IApplicationShutDownService
    {
        public void ShutDown()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
