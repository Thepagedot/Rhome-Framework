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
            var channel = GetItem(position);
            var view = HomeMaticLayout.For(Context, channel);
            view.FindViewById<TextView>(Resource.Id.tvName).Text = channel.Name;

            return view;
        }            
    }
}   