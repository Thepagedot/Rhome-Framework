
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
using Thepagedot.Rhome.HomeMatic.Services;
using Thepagedot.Rhome.HomeMatic.Models;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Demo.Droid
{
    [Activity(Label = "Settings", ParentActivity = typeof(MainActivity))]			
	public class SettingsActivity : AppCompatActivity
	{        
        ListView lvCentralUnits;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            lvCentralUnits = FindViewById<ListView>(Resource.Id.lvCentralUnits);
            lvCentralUnits.Adapter = new CentralUnitAdapter(DataHolder.Current.CentralUnits);
            lvCentralUnits.ItemClick += (sender, e) => ShowAddEditDialog(DataHolder.Current.CentralUnits.ElementAt(e.Position));
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
                //Settings.SaveHomeMaticIpAddress(this, address);
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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {            
            switch (item.ItemId)
            {
                case Resource.Id.menu_add:
                    ShowAddEditDialog(null);             
                    break;
                case Resource.Id.menu_rescan:
                    Rescan();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ShowAddEditDialog(CentralUnit centralUnit)
        {            
            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            var dialogView = LayoutInflater.Inflate(Resource.Layout.AddHomeControlSystemDialog, null);
            builder.SetView(dialogView);
            builder.SetNeutralButton(Android.Resource.String.Cancel, (sender, e) => {});

            if (centralUnit == null)
            {
                // New central unit will be added    
                builder.SetTitle(Resource.String.add_home_control_system_title);
                builder.SetPositiveButton(Android.Resource.String.Ok, AddCentralUnitButton_Clicked);
            }
            else
            {
                // Existing unit will be edited
                builder.SetTitle(Resource.String.edit_home_control_system_title);
                builder.SetPositiveButton(Android.Resource.String.Ok, (sender, e) => EditCentralUnitButton_Clicked(sender, e, centralUnit));
                builder.SetNegativeButton(Resource.String.delete, (sender, e) => DeleteCentralUnitButton_Clicked(sender, e, centralUnit));

                // Fill controls with properties of the existing central unit
                dialogView.FindViewById<EditText>(Resource.Id.etName).Text = centralUnit.Name;
                dialogView.FindViewById<EditText>(Resource.Id.etIpAddress).Text = centralUnit.Address;
                dialogView.FindViewById<Spinner>(Resource.Id.spBrand).SetSelection((int)centralUnit.Brand);
                dialogView.FindViewById<Spinner>(Resource.Id.spBrand).Enabled = false;
            }

            builder.Show();
        }


        private async void AddCentralUnitButton_Clicked(object sender, DialogClickEventArgs e) 
        {
            var name = (sender as Android.Support.V7.App.AlertDialog).FindViewById<EditText>(Resource.Id.etName).Text;
            var address = (sender as Android.Support.V7.App.AlertDialog).FindViewById<EditText>(Resource.Id.etIpAddress).Text;
            DataHolder.Current.CentralUnits.Add(new Ccu(name, address));

            await UpdateAndSaveAsync();
        }

        private async void EditCentralUnitButton_Clicked(object sender, DialogClickEventArgs e, CentralUnit centralUnit)
        {
            var name = (sender as Android.Support.V7.App.AlertDialog).FindViewById<EditText>(Resource.Id.etName).Text;
            var address = (sender as Android.Support.V7.App.AlertDialog).FindViewById<EditText>(Resource.Id.etIpAddress).Text;

            centralUnit.Name = name;
            centralUnit.Address = address;

            await UpdateAndSaveAsync();
        }

        private async void DeleteCentralUnitButton_Clicked(object sender, DialogClickEventArgs e, CentralUnit centralUnit)
        {
            DataHolder.Current.CentralUnits.Remove(centralUnit);
            await UpdateAndSaveAsync();
        }

        /// <summary>
        /// Updates the ListView's controller and saves the changes to the settings
        /// </summary>
        /// <returns>Task to await</returns>
        private async Task UpdateAndSaveAsync()
        {
            // Update list
            (lvCentralUnits.Adapter as CentralUnitAdapter).NotifyDataSetChanged();

            // Save changes in settings
            Settings.Configuration.CentralUnits = DataHolder.Current.CentralUnits;
            await Settings.SaveSettingsAsync();
        }

        private async void Rescan()
        {
            Settings.Configuration.Rooms.Clear();
            await Settings.SaveSettingsAsync();
            await DataHolder.Current.Init();
        }
	}
}