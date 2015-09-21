using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Demo.Win.ViewModels;
using Thepagedot.Rhome.Demo.Win.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Thepagedot.Rhome.Demo.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.NavigationCacheMode = NavigationCacheMode.Required;

            App.InitalizeStatusBar();
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SettingsViewModel.Current.LoadSettings();
            await MainViewModel.Current.LoadDataAsync();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void AppBarSettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RoomListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedRoom = e.ClickedItem as Room;
            if (clickedRoom != null)
            {
                Frame.Navigate(typeof(RoomView), clickedRoom);
            }
        }
    }
}
