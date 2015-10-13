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
    [Activity(Label = "Rhome", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;

        protected override void OnCreate(Bundle bundle)
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

            // Demo Data
            var ccu = new Ccu("HomeMatic Robby", "192.168.0.14");
            var homematic = new HomeMaticXmlApi(ccu);
            DataHolder.Current.CurrentHomeControl = homematic;
        }

        protected override async void OnResume()
        {
            base.OnResume();

            var homeControl = DataHolder.Current.CurrentHomeControl as HomeMaticXmlApi;
            var rooms = await homeControl.GetRoomsAsync();

            var lvRooms = FindViewById<ListView>(Resource.Id.lvRooms);
            lvRooms.Adapter = new RoomAdapter(this, 0, rooms.ToList());
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            Toast.MakeText(this, e.MenuItem.TitleFormatted + " clicked.", ToastLength.Short).Show();
            drawerLayout.CloseDrawers();
        }
    }
}