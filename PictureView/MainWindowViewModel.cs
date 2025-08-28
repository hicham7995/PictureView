using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureView
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel(MainWindow view)
        {
            this.QuitCommand = new BaseCommand(() => { view.Close(); });
        }
        public BaseCommand QuitCommand
        {
            get;
            set;
        }
    }
}
