using System;
using Android.Widget;
using Thepagedot.Rhome.HomeMatic.Models;
using Android.Content;
using Android.Views;
using System.Collections.Generic;

namespace Thepagedot.Rhome.Demo.Droid
{
    public class HomeMaticChannelAdapter : ArrayAdapter<HomeMaticChannel>
    {
        public HomeMaticChannelAdapter(Context context, int resourceId, IList<HomeMaticChannel> items) : base(context, resourceId, items) {}

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var view = LayoutInflater.From(Context).Inflate(Resource.Layout.Channel, null);
            var channel = GetItem(position);

            view.FindViewById<TextView>(Resource.Id.tvName).Text = channel.Name;

            return view;
        }
    }
}

