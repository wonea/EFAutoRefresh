// Copyright © 2012 by blueshell Software Engineering Harry von Borstel (http://www.blueshell.com)
// This work is licensed under COPL (see http://www.codeproject.com/info/cpol10.aspx)
// 
// 
using EFAutoRefresh;
using MyChat.Model;
using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Runtime.CompilerServices;

namespace MyChat
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private readonly ChatContext _myObjectContext;

        public MainViewModel()
        {
            if (App.Current != null)
            {
                _myObjectContext = App.Current.MyContext;
                Chat = new AutoRefreshWrapper<Chat>(_myObjectContext.Chats, _myObjectContext, RefreshMode.StoreWins);
            }
        }

        public string Name { get; set; }

        private string _message;
        public string Message { get { return _message; } set { _message = value; RaisePropertyChanged(); } }

        public AutoRefreshWrapper<Chat> Chat { get; set; }

        internal void Send()
        {
            _myObjectContext.Chats.Add(new Chat { Date = DateTime.Now, Name = Name, Message = Message });
            _myObjectContext.SaveChanges();
            Message = null;
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
