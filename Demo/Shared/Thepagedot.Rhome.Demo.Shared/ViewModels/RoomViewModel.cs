using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Demo.Shared.ViewModels
{
    public class RoomViewModel : AsyncViewModelBase
    {
        private Room _CurrentRoom;
        public Room CurrentRoom
        {
            get { return _CurrentRoom; }
            set { _CurrentRoom = value; RaisePropertyChanged(); }
        }

        public RoomViewModel()
        {

        }

    }
}
