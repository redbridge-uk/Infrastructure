using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class ToolbarItemFactory
    {
        public ToolbarItem CreateToolbarItem(IToolbarItemViewModel tb)
        {
            if (tb == null) throw new ArgumentNullException(nameof(tb));
            var item = new ToolbarItem()
            {
                Command = tb.Command,
                CommandParameter = tb.CommandParameter,
                Icon = tb.Icon,
                Text = tb.Text,
                Priority = tb.Position == ToolbarItemOrder.Secondary ? 0 : 1, // Renderer for iOS will switch secondaries to the right hand side.
            };
            item.BindingContext = tb;
            item.SetBinding(MenuItem.TextProperty, "Text");
            return item;
        }
    }
}
