using Microsoft.Maui.Controls;
using System.Reflection;

namespace BlazorBindings.UnitTests.Extensions;

public static class ControlsExtensions
{
    public static T GetTemplateContent<T>(this TemplatedView view)
    {
        return (T)((Layout)view.Children[0]).Children[0];
    }

    public static T GetTemplateContent<T>(this object templateRoot)
    {
        return (T)((Layout)templateRoot).Children[0];
    }

    public static void RaiseEvent<T>(this BindableObject bindableObject, string eventName, T args)
    {
        var declaringType = bindableObject.GetType().GetEvent(eventName).DeclaringType;
        var backingField = declaringType.GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        var delegateInstance = (EventHandler<T>)backingField.GetValue(bindableObject);
        delegateInstance?.Invoke(null, args);
    }
}
