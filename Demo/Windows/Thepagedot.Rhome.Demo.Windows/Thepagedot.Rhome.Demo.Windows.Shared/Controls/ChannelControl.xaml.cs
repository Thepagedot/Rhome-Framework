using System;
using System.ComponentModel;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Demo.Win.ViewModels;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Thepagedot.Rhome.Demo.Win.Controls
{
    public sealed partial class ChannelControl : UserControl
    {
        public static readonly DependencyProperty DpChannel = DependencyProperty.Register("Channel", typeof(Object), typeof(ChannelControl), new PropertyMetadata(default(Object)));
        public static readonly DependencyProperty DpTest    = DependencyProperty.Register("Test", typeof(string), typeof(ChannelControl), new PropertyMetadata(default(string)));

        public Object Channel
        {
            get { return (Object)this.GetValue(DpChannel); }
            set { this.SetValueDp(DpChannel, value); }
        }

        public string Test
        {
            get { return (string)this.GetValue(DpTest); }
            set { this.SetValueDp(DpTest, value); }
        }

        public ChannelControl()
        {
            this.InitializeComponent();
            if (DesignMode.DesignModeEnabled)
                LoadDemoData();
            (this.Content as FrameworkElement).DataContext = this;
        }

        private void LoadDemoData()
        {
            Channel = MainViewModel.Current.RoomList.First().DeviceList.First().ChannelList.First();
            Test = "Hallo hallo Test Test";
        }

        private async void OnOffSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = (sender as ToggleSwitch);
            if (toggleSwitch != null)
            {
                var channel = toggleSwitch.DataContext as Channel;
                if (channel != null)
                {
                    toggleSwitch.IsEnabled = false;
                    await App.HomeMaticXmlApi.SendChannelUpdateAsync(channel, (sender as ToggleSwitch).IsOn);
                    toggleSwitch.IsEnabled = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void SetValueDp(DependencyProperty property, object value, [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

    }
}
