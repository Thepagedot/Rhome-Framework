
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
using Thepagedot.Rhome.HomeMatic.Models;

namespace Thepagedot.Rhome.Demo.Droid
{	
    public static class HomeMaticLayout
    {
        public static View For(Context context, HomeMaticChannel channel)
        {               
            if (channel is Switcher)
                return GetViewForSwitcher(context, channel);
            if (channel is Contact)
                return GetViewForContact(context, channel);
            if (channel is DoorHandle)
                return GetViewForDoorHandle(context, channel);
            if (channel is Information)
                return GetViewForInformation(context, channel);                
            if (channel is TemperatureSlider)
                return GetViewForTemperatureSlider(context, channel);

            return LayoutInflater.From(context).Inflate(Resource.Layout.Channel, null);
        }

        private static View GetViewForSwitcher(Context context, HomeMaticChannel channel)
        {
            var view = LayoutInflater.From(context).Inflate(Resource.Layout.Switcher, null);
            var tbSwitcher = view.FindViewById<ToggleButton>(Resource.Id.tbSwitcher);
            tbSwitcher.Checked = (channel as Switcher).State;
            tbSwitcher.CheckedChange += async delegate(object sender, CompoundButton.CheckedChangeEventArgs e)
            {
                if (e.IsChecked)
                    await (channel as Switcher).OnAsync(DataHolder.Current.HomeMaticApi);
                else
                    await (channel as Switcher).OffAsync(DataHolder.Current.HomeMaticApi);
            };

            return view;
        }

        private static View GetViewForContact(Context context, HomeMaticChannel channel)
        {
            var view = LayoutInflater.From(context).Inflate(Resource.Layout.DoorHandle, null);
            var state = (channel as Contact).State;

            if (state)
            {
                view.FindViewById<TextView>(Resource.Id.tvState).Text = context.GetString(Resource.String.open);
                view.FindViewById<ImageView>(Resource.Id.ivOpen).Visibility = ViewStates.Visible;
                view.FindViewById<ImageView>(Resource.Id.ivClosed).Visibility = ViewStates.Gone;
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.tvState).Text = context.GetString(Resource.String.closed);
                view.FindViewById<ImageView>(Resource.Id.ivOpen).Visibility = ViewStates.Gone;
                view.FindViewById<ImageView>(Resource.Id.ivClosed).Visibility = ViewStates.Visible;
            }

            return view;
        }

        private static View GetViewForDoorHandle(Context context, HomeMaticChannel channel)
        {
            var view = LayoutInflater.From(context).Inflate(Resource.Layout.DoorHandle, null);
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

        private static View GetViewForInformation(Context context, HomeMaticChannel channel)
        {
            var view = LayoutInflater.From(context).Inflate(Resource.Layout.Information, null);
            view.FindViewById<TextView>(Resource.Id.tvContent).Text = (channel as Information).Content;
            return view;
        }

        private static View GetViewForTemperatureSlider(Context context, HomeMaticChannel channel)
        {
            var temperatureSlider = (channel as TemperatureSlider);
            var view = LayoutInflater.From(context).Inflate(Resource.Layout.TemperatureSlider, null);
            view.FindViewById<TextView>(Resource.Id.tvState).Text = temperatureSlider.Value + temperatureSlider.Unit;
            view.FindViewById<SeekBar>(Resource.Id.sbTemperature).Progress = Convert.ToInt32(temperatureSlider.Value);
            view.FindViewById<SeekBar>(Resource.Id.sbTemperature).ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                view.FindViewById<TextView>(Resource.Id.tvState).Text = e.Progress + temperatureSlider.Unit;
            };
            view.FindViewById<SeekBar>(Resource.Id.sbTemperature).StopTrackingTouch += async (object sender, SeekBar.StopTrackingTouchEventArgs e) => 
                    await temperatureSlider.ChangeTemperatureAsync(e.SeekBar.Progress, DataHolder.Current.HomeMaticApi);

            return view;
        }
    }
}