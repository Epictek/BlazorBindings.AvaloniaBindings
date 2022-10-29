using Microsoft.AspNetCore.Components;
using System;
using System.Windows.Input;

namespace BlazorBindings.Maui.Elements.Input
{
    internal class EventCallbackCommand : ICommand
    {
        private readonly EventCallback _eventCallback;

        public EventCallbackCommand(EventCallback eventCallback)
        {
            _eventCallback = eventCallback;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _eventCallback.HasDelegate;

        public void Execute(object parameter)
        {
            _ = _eventCallback.InvokeAsync();
        }
    }
}
