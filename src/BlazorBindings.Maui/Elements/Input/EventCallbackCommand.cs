using System;
using System.Windows.Input;

namespace BlazorBindings.Maui.Elements.Input
{
    internal class EventCallbackCommand : ICommand
    {
        private readonly Action _eventCallbackAction;

        public EventCallbackCommand(Action eventCallbackAction)
        {
            _eventCallbackAction = eventCallbackAction;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _eventCallbackAction();
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
