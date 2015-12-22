using System;
using Thepagedot.Rhome.HomeMatic.Models;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Views;
using System.Linq;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Demo.Droid
{
    public class DeviceAdapter : ArrayAdapter<Device>
    {
        public DeviceAdapter(Context context, int resourceId, IList<Device> items) : base(context, resourceId, items) {}

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(Context).Inflate(Resource.Layout.Device, null);
            var device = GetItem(position);

            view.FindViewById<TextView>(Resource.Id.tvName).Text = device.Name;

            if (device is HomeMaticDevice)
            {             
                var homeMaticDevice = (HomeMaticDevice)device;

                // Show low battery indicator, when at leat one channel has low battery
                if (homeMaticDevice.Channels.FirstOrDefault(c => c.IsLowBattery == true) != null)
                    view.FindViewById<ImageView>(Resource.Id.ivLowBat).Visibility = ViewStates.Visible;

                // Add channels to the list
                var channels = homeMaticDevice.Channels.Where(c => c.IsVisible).ToList();
                var adapter = new HomeMaticChannelAdapter(Context, 0, channels);
                var lvChannels = view.FindViewById<ListView>(Resource.Id.lvChannels);
                lvChannels.Adapter = adapter;
                ScollingHelpers.SetListViewHeightBasedOnChildren(lvChannels, 0);                
            }

            return view;
        }
    }
}

