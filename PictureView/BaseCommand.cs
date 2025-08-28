using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureView
{
    public class BaseCommand : ICommand
    {
        public Action Action { get; }

        public event EventHandler? CanExecuteChanged;

        public BaseCommand(Action action)
        {
            this.Action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this.Action();
        }
    }
}
