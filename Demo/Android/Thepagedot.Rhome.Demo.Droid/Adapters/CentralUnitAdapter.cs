using System;
using Thepagedot.Rhome.Base.Interfaces;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Views;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Demo.Droid
{
    public class CentralUnitAdapter : BaseAdapter<CentralUnit>
    {
        private List<CentralUnit> items;      

        public CentralUnitAdapter(List<CentralUnit> items) : base() 
        {
            this.items = items;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(parent.Context).Inflate(Resource.Layout.HomeControlApiItem, parent, false);

            view.FindViewById<TextView>(Resource.Id.tvName).Text = GetItem(position).Name;
            view.FindViewById<TextView>(Resource.Id.tvAddress).Text = GetItem(position).Address;
            view.FindViewById<TextView>(Resource.Id.tvBrand).Text = GetItem(position).Brand.ToString();

            return view;
        }

        private CentralUnit GetItem(int position)
        {
            return items[position];
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override CentralUnit this[int position]
        {
            get { return items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }            
    }
}