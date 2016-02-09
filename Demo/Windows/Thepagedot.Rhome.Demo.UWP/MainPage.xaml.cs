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
using Windows.UI.Popups;
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
        private int _NumberOfColumns = 2;

        public MainPage()
        {
            this.InitializeComponent();
            App.Bootstrapper.MainViewModel.ConnectionError += MainViewModel_ConnectionError;
        }

        private async void MainViewModel_ConnectionError(object sender, Shared.ViewModels.ConnectionErrorEventArgs e)
        {
            var dialog = new MessageDialog(e.Message, e.Title);
            await dialog.ShowAsync();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Init MainViewModel
            await App.Bootstrapper.MainViewModel.InitializeAsync();

            // Initially set number od columns to the current state's value
            SetNumberOfColumnsByState(VisualStateManager.GetVisualStateGroups(MainGrid).First().CurrentState);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerSplitView.IsPaneOpen = !HamburgerSplitView.IsPaneOpen;
        }

        private void gvRooms_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Set clicked room as current room
            var room = e.ClickedItem as Room;
            App.Bootstrapper.RoomViewModel.CurrentRoom = room;

            // Navigate to room page
            Frame.Navigate(typeof(RoomPage));
        }

        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        #region Room Grid

        private void gvRooms_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Make sure that the item's width is alsways half the size
            ((ItemsWrapGrid)gvRooms.ItemsPanelRoot).ItemWidth = e.NewSize.Width / _NumberOfColumns - gvRooms.Padding.Left;
        }

        private void VisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            SetNumberOfColumnsByState(e.NewState);
        }

        private void SetNumberOfColumnsByState(VisualState state)
        {
            if (state.Name.Equals(VisualStatePhone.Name))
                _NumberOfColumns = 2;
            else if (state.Name.Equals(VisualStateTablet.Name))
                _NumberOfColumns = 3;
            else if (state.Name.Equals(VisualStateDesktop.Name))
                _NumberOfColumns = 4;
        }

        #endregion
    }
}
