using System;
using Android.Widget;
using Thepagedot.Rhome.Base.Models;
using System.Collections.Generic;
using Android.Content;
using Android.Views;

namespace Thepagedot.Rhome.Demo.Droid
{
    public class RoomAdapter : ArrayAdapter<Room>
    {
        public RoomAdapter(Context context, int resourceId, IList<Room> items) : base(context, resourceId, items)
        {
        }

        public override View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.RoomItem, null);

            view.FindViewById<TextView>(Resource.Id.tvRoomName).Text = GetItem(position).Name;

            return view;
        }
    }
}   