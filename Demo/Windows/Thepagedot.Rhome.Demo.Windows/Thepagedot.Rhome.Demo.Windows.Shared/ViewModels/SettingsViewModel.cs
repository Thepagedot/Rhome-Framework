using Thepagedot.Rhome.Demo.Shared.Models;
using Thepagedot.Rhome.Demo.Win.Common;
using Thepagedot.Rhome.HomeMatic.Models;
using Thepagedot.Rhome.HomeMatic.Services;

namespace Thepagedot.Rhome.Demo.Win.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private static SettingsViewModel _Current;
        public static SettingsViewModel Current
        {
            get { return _Current ?? (_Current = new SettingsViewModel()); }
        }

        private SettingsConfiguration _SettingsConfiguration;
        public SettingsConfiguration SettingsConfiguration
        {
            get { return _SettingsConfiguration ?? (_SettingsConfiguration = new SettingsConfiguration()); }
            set { SetProperty(ref _SettingsConfiguration, value); }
        }

        public SettingsViewModel()
        {
            _Current = this;
        }

        public void LoadSettings()
        {
            SettingsConfiguration.HomeMaticSettings = new HomeMaticSettings("192.168.0.14");
            //SettingsConfiguration.HomeMaticSettings = new HomeMaticSettings("192.168.127.16");
            var homeMaticCcu = new Ccu("My HomeMatic", SettingsConfiguration.HomeMaticSettings.Address);
            App.HomeMaticXmlApi = new HomeMaticXmlApi(homeMaticCcu);
        }
    }
}
