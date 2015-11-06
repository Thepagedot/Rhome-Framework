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

namespace Thepagedot.Rhome.Demo.Droid
{
    [Activity(Label = "Rhome", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (DataHolder.Current == null)
                new DataHolder();         

            SetContentView(Resource.Layout.Main);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();          

            // Load data
            await DataHolder.Current.Init();

            var gvRooms = FindViewById<GridView>(Resource.Id.gvRooms);
            gvRooms.Adapter = new RoomAdapter(this, 0, DataHolder.Current.Rooms);
            gvRooms.ItemClick += GvRooms_ItemClick;
            ScollingHelpers.SetListViewHeightBasedOnChildren(gvRooms, Resources.GetDimension(Resource.Dimension.default_margin));           
        }

        void GvRooms_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
        {
            DataHolder.Current.CurrentRoom = DataHolder.Current.Rooms.ElementAt(e.Position);
            StartActivity(new Intent(this, typeof(RoomActivity)));
        }           

        protected override void OnResume()
        {
            base.OnResume();
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            Toast.MakeText(this, e.MenuItem.TitleFormatted + " clicked.", ToastLength.Short).Show();
            drawerLayout.CloseDrawers();
        }
    }      
}