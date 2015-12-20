using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.HomeMatic.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Thepagedot.Rhome.Demo.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += (sender, args) => { if (Frame.CanGoBack) Frame.GoBack(); };
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await App.Bootstrapper.SettingsViewModel.InitializeAsync();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private async void AddCentralUnitMenu_Click(object sender, RoutedEventArgs e)
        {
            await AddEditHomeControlSystemDialog.ShowAsync();
        }

        private async void AddEditHomeControlSystemDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // TODO: Error handline
            // TODO: differen Brands
            var ccu = new Ccu(tbxName.Text, tbxAddress.Text);
            await App.Bootstrapper.SettingsViewModel.AddCentralUnitAsync(ccu);
        }

        private async void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var centralUnit = (CentralUnit)(sender as FrameworkElement).DataContext;
            if (centralUnit != null)
            {
                await App.Bootstrapper.SettingsViewModel.DeleteCentralUnitAsync(centralUnit);
            }
        }

        private void Border_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var element = (sender as FrameworkElement);
            var flyout = FlyoutBase.GetAttachedFlyout(element);
            flyout.ShowAt(element);
        }

        private void Border_Holding(object sender, HoldingRoutedEventArgs e)
        {
            var element = (sender as FrameworkElement);
            var flyout = FlyoutBase.GetAttachedFlyout(element);
            flyout.ShowAt(element);
        }
    }
}
