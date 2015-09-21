using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thepagedot.Rhome.Demo.Win.Common
{
    public class ChannelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SwitcherTemplate { get; set; }
        public DataTemplate ContactTemplate { get; set; }
        public DataTemplate DoorHandleTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is HomeMatic.Models.Switcher)
            {
                return SwitcherTemplate;
            }
            if (item is HomeMatic.Models.Contact)
            {
                return ContactTemplate;
            }
            if (item is HomeMatic.Models.DoorHandle)
                return DoorHandleTemplate;

            return base.SelectTemplateCore(item, container);
        }
    }
}
