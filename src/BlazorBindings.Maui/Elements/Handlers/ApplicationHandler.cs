using Microsoft.Maui.Controls;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    internal class ApplicationHandler : IMauiContainerElementHandler
    {
        private readonly Application _application;

        public ApplicationHandler(Application application)
        {
            _application = application;
        }

        public void AddChild(MC.BindableObject child, int physicalSiblingIndex)
        {
            _application.MainPage = (MC.Page)child;
        }

        public int GetChildIndex(MC.BindableObject child)
        {
            return Equals(_application.MainPage, child) ? 0 : -1;
        }

        public void RemoveChild(MC.BindableObject child)
        {
            // It is not allowed to have no MainPage.
        }

        public MC.BindableObject ElementControl => _application;
        public object TargetElement => _application;
        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
    }
}
