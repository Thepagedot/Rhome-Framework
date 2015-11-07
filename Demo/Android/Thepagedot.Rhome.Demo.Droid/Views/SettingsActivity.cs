
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
using System.Net;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Demo.Droid
{
    [Activity(Label = "Settings", ParentActivity = typeof(MainActivity))]			
	public class SettingsActivity : AppCompatActivity
	{        
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            FindViewById<Button>(Resource.Id.btnCheckAddress).Click += btnCheckAddress_Click;
		}

        protected override void OnResume()
        {
            base.OnResume();

            // Init
            var address = Settings.GetHomeMaticIpAddress(this);
            if (address != null)
            {
                FindViewById<EditText>(Resource.Id.etIpAddress).Text = address;
            } 
        }

        async void btnCheckAddress_Click (object sender, EventArgs e)
        {
            var address = FindViewById<EditText>(Resource.Id.etIpAddress).Text;
            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetNeutralButton(Android.Resource.String.Ok, (s, ev) => {});

            if (await CheckConnection(address))
            {                
                builder.SetTitle(Resource.String.connection_successful_title);
                builder.SetMessage(Resource.String.connection_successful_message);

                // Save settings
                Settings.SaveHomeMaticIpAddress(this, address);
            }
            else
            {
                builder.SetTitle(Resource.String.connection_failed_title);
                builder.SetMessage(Resource.String.connection_failed_message);
            }

            builder.Show();
        }

        private async Task<bool> CheckConnection(string address)
        {
            // Check if input is a valid IP address
            IPAddress ip;
            if (!IPAddress.TryParse(address, out ip))
                return false;

            // Check if connection to CCU is valid
            DataHolder.Current.HomeMaticApi = new Thepagedot.Rhome.HomeMatic.Services.HomeMaticXmlApi(new Thepagedot.Rhome.HomeMatic.Models.Ccu("Test", address));
            if (!await DataHolder.Current.HomeMaticApi.CheckConnectionAsync())
                return false;

            return true;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SettingsMenu, menu);
            return true;
        }            
	}
}

