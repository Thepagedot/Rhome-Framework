
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;

namespace Thepagedot.Rhome.Demo.Droid
{
    [Activity(Label = "Room", ParentActivity = typeof(MainActivity))]			
    public class RoomActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Room);
            Title = DataHolder.Current.CurrentRoom.Name;

            // Init toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Init swipe to refresh
            var slSwipeContainer = FindViewById<SwipeRefreshLayout>(Resource.Id.slSwipeContainer);
            slSwipeContainer.SetColorSchemeResources(Android.Resource.Color.HoloBlueBright, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloOrangeLight, Android.Resource.Color.HoloRedLight);
            slSwipeContainer.Refresh += SlSwipeContainer_Refresh;

            var lvDevices = FindViewById<ListView>(Resource.Id.lvDevices);
            lvDevices.Adapter = new DeviceAdapter(this, 0, DataHolder.Current.CurrentRoom.Devices);
        }

        async void SlSwipeContainer_Refresh (object sender, EventArgs e)
        {
            await DataHolder.Current.UpdateCurrentRoom();
            (sender as SwipeRefreshLayout).Refreshing = false;
            (FindViewById<ListView>(Resource.Id.lvDevices).Adapter as DeviceAdapter).NotifyDataSetChanged();
        }
    }
}

