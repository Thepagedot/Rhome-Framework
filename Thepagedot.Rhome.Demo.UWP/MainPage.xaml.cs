using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Demo.UWP.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Thepagedot.Rhome.Demo.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ((Bootstrapper)Application.Current.Resources["Bootstrapper"]).MainViewModel.Initialize();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerSplitView.IsPaneOpen = !HamburgerSplitView.IsPaneOpen;
        }

        private void gvRooms_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Set clicked room as current room
            var room = e.ClickedItem as Room;
            ((Bootstrapper)Application.Current.Resources["Bootstrapper"]).RoomViewModel.CurrentRoom = room;

            // Navigate to room page
            Frame.Navigate(typeof(RoomPage));
        }

        private void gvRooms_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Make sure that the item's width is alsways half the size
            ((ItemsWrapGrid)gvRooms.ItemsPanelRoot).ItemWidth = e.NewSize.Width / 2 - gvRooms.Padding.Left;
        }
    }
}
