using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Thepagedot.Rhome.HomeMatic.Models;
using Thepagedot.Rhome.HomeMatic.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Demo.Droid
{
    [Activity(Label = "Rhome", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init navigation drawer
            FindViewById<NavigationView>(Resource.Id.nav_view).NavigationItemSelected += NavigationView_NavigationItemSelected;
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            // Init swipe to refresh
            var slSwipeContainer = FindViewById<SwipeRefreshLayout>(Resource.Id.slSwipeContainer);
            slSwipeContainer.SetColorSchemeResources(Android.Resource.Color.HoloBlueBright, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloOrangeLight, Android.Resource.Color.HoloRedLight);
            slSwipeContainer.Refresh += SlSwipeContainer_Refresh;

            // Init DataHolder
            if (DataHolder.Current == null) new DataHolder();
            await DataHolder.Current.Init();

            // Init GridView
            var gvRooms = FindViewById<GridView>(Resource.Id.gvRooms);
            gvRooms.Adapter = new RoomAdapter(this, 0, DataHolder.Current.Rooms);
            gvRooms.ItemClick += GvRooms_ItemClick;
			ScollingHelpers.SetListViewHeightBasedOnChildren(gvRooms, Resources.GetDimension(Resource.Dimension.default_margin));        

            // Update status
            await DataHolder.Current.Update();
        }                        

		protected override void OnResume()
		{
			base.OnResume();
			var gvRooms = FindViewById<GridView>(Resource.Id.gvRooms);
			ScollingHelpers.SetListViewHeightBasedOnChildren(gvRooms, Resources.GetDimension(Resource.Dimension.default_margin));
		}

        async void SlSwipeContainer_Refresh (object sender, EventArgs e)
		{
			var gvRooms = FindViewById<GridView>(Resource.Id.gvRooms);
			ScollingHelpers.SetListViewHeightBasedOnChildren(gvRooms, Resources.GetDimension(Resource.Dimension.default_margin));
            await DataHolder.Current.Update();
            (sender as SwipeRefreshLayout).Refreshing = false;
        }            

        void GvRooms_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
        {
            DataHolder.Current.CurrentRoom = DataHolder.Current.Rooms.ElementAt(e.Position);
            StartActivity(new Intent(this, typeof(RoomActivity)));
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            drawerLayout.CloseDrawers();

            switch (e.MenuItem.ItemId)
            {
                default:
                    Toast.MakeText(this, e.MenuItem.TitleFormatted + " clicked.", ToastLength.Short).Show();
                    break;
                case Resource.Id.nav_settings:
                    StartActivity(new Intent(this, typeof(SettingsActivity)));
                    break;                   
            }
        }
    }      
}