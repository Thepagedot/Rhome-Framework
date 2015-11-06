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
            var view = CreateViewFor(Context, channel);
            view.FindViewById<TextView>(Resource.Id.tvName).Text = channel.Name;

            return view;
        }

        private View CreateViewFor(Context context, HomeMaticChannel channel)
        {
            View view;
            if (channel is Switcher)
            {
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.Switcher, null);
                var tbSwitcher = view.FindViewById<ToggleButton>(Resource.Id.tbSwitcher);
                tbSwitcher.Checked = (channel as Switcher).State;
                tbSwitcher.CheckedChange += async delegate(object sender, CompoundButton.CheckedChangeEventArgs e)
                {
                        if (e.IsChecked)
                            await (channel as Switcher).On(DataHolder.Current.HomeMaticApi);
                        else
                            await (channel as Switcher).Off(DataHolder.Current.HomeMaticApi);
                };

                return view;
            }

            if (channel is DoorHandle)
            {
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.DoorHandle, null);
                var state = (channel as DoorHandle).State;

                switch (state)
                {
                    case DoorHandleState.Open:
                        view.FindViewById<TextView>(Resource.Id.tvState).Text = context.GetString(Resource.String.open);
                        view.FindViewById<ImageView>(Resource.Id.ivOpen).Visibility = ViewStates.Visible;
                        view.FindViewById<ImageView>(Resource.Id.ivClosed).Visibility = ViewStates.Gone;
                        break;
                    case DoorHandleState.Tilted:
                        view.FindViewById<TextView>(Resource.Id.tvState).Text = context.GetString(Resource.String.tilted);
                        view.FindViewById<ImageView>(Resource.Id.ivOpen).Visibility = ViewStates.Visible;
                        view.FindViewById<ImageView>(Resource.Id.ivClosed).Visibility = ViewStates.Gone;
                        break;
                    case DoorHandleState.Closed:
                        view.FindViewById<TextView>(Resource.Id.tvState).Text = context.GetString(Resource.String.closed);
                        view.FindViewById<ImageView>(Resource.Id.ivOpen).Visibility = ViewStates.Gone;
                        view.FindViewById<ImageView>(Resource.Id.ivClosed).Visibility = ViewStates.Visible;
                        break;
                }

                return view;
            }

            return LayoutInflater.From(Context).Inflate(Resource.Layout.Channel, null);
        }
    }
}   