using System;
using Thepagedot.Rhome.HomeMatic.Models;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Views;
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
                var adapter = new HomeMaticChannelAdapter(Context, 0, ((HomeMaticDevice)device).ChannelList);
                view.FindViewById<ListView>(Resource.Id.lvChannels).Adapter = adapter;
            }

            return view;
        }
    }
}

