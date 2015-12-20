using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Demo.Shared.ViewModels
{
    public class AsyncViewModelBase : ViewModelBase
    {
        #region Events

        public event ConnectionErrorEventHandler ConnectionError;
        public delegate void ConnectionErrorEventHandler(object sender, ConnectionErrorEventArgs e);
        public void RaiseConnectionError(string title, string message)
        {
            if (ConnectionError != null)
                ConnectionError(this, new ConnectionErrorEventArgs(title, message));
        }

        #endregion


        private bool _IsLoading = false;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; RaisePropertyChanged(); }
        }

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get { return _IsLoaded; }
            set { _IsLoaded = value; RaisePropertyChanged(); }
        }
    }

    public class ConnectionErrorEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public ConnectionErrorEventArgs(string title, string message) : base()
        {
            this.Title = title;
            this.Message = message;
        }
    }
}
