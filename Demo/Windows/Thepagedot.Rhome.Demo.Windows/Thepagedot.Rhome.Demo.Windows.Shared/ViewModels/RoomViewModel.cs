using System.Linq;
using Windows.ApplicationModel;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Demo.Win.Common;

namespace Thepagedot.Rhome.Demo.Win.ViewModels
{
    public class RoomViewModel : BindableBase
    {
        private static RoomViewModel _Current;
        public static RoomViewModel Current
        {
            get { return _Current ?? (_Current = new RoomViewModel()); }
        }

        private Room _CurrentRoom;
        public Room CurrentRoom
        {
            get { return _CurrentRoom; }
            set { SetProperty(ref _CurrentRoom, value); }
        }

        public RoomViewModel ()
        {
            _Current = this;
            if (DesignMode.DesignModeEnabled)
                LoadDemoData();
        }

        private void LoadDemoData()
        {
            CurrentRoom = MainViewModel.Current.RoomList.First();
        }
    }
}
