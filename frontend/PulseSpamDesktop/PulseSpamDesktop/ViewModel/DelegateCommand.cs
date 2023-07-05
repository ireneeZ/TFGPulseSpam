using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PulseSpamDesktop.ViewModel
{
    public class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }

        public Func<bool>? CanExecuteAction { get; set; }

        public void Execute(object? parameter)
        {
            if (CommandAction != null)
            {
                CommandAction();
            }
        }

        public bool CanExecute(object? parameter)
        {
            return CanExecuteAction == null || CanExecuteAction();
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action commandAction, Func<bool> canExecuteAction)
        {
            CommandAction = commandAction;
            CanExecuteAction = canExecuteAction;
        }

        public DelegateCommand(Action commandAction)
        {
            CommandAction = commandAction;
        }
    }
}
