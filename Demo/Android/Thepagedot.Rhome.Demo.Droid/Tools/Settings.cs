using System;
using Android.Content;

namespace Thepagedot.Rhome.Demo.Droid
{
    public static class Settings
    {
        public static string GetHomeMaticIpAddress(Context context)
        {
            var homeMaticSettings = context.GetSharedPreferences("HomeMaticSettings", 0);
            var address = homeMaticSettings.GetString("ipAddress", null);
            return address;
        }

        public static void SaveHomeMaticIpAddress(Context context, string address)
        {
            var homeMaticSettings = context.GetSharedPreferences("HomeMaticSettings", 0);
            var editor = homeMaticSettings.Edit();
            editor.PutString("ipAddress", address);
            editor.Commit();
        }
    }
}

