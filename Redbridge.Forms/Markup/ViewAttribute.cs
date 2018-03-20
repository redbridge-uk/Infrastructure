using System;
namespace Redbridge.Forms.Markup
{
    /// <summary>
    /// Attribute your view model classes and indicate the view you want to use to display them.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ViewAttribute : Attribute
    {
        public ViewAttribute(Type viewType)
        {
            ViewType = viewType ?? throw new ArgumentNullException(nameof(viewType));
        }

        public Type ViewType { get; }
    }
}
