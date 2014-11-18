// Copyright © 2012 by blueshell Software Engineering Harry von Borstel (http://www.blueshell.com)
// This work is licensed under COPL (see http://www.codeproject.com/info/cpol10.aspx)
// 
// 
using EFAutoRefresh;
using MyChat.Model;
using System;
using System.ComponentModel;
using System.Data.Objects;
using System.Runtime.CompilerServices;

namespace MyChat
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private readonly ChatContext myObjectContext;

        public MainViewModel()
        {
            if (App.Current != null)
            {
                myObjectContext = App.Current.MyContext;
                this.Chat = new AutoRefreshWrapper<Chat>(myObjectContext.Chats, myObjectContext, RefreshMode.StoreWins);
            }
        }

        public string Name { get; set; }

        private string _Message;
        public string Message { get { return _Message; } set { _Message = value; RaisePropertyChanged(); } }

        public AutoRefreshWrapper<Chat> Chat { get; set; }

        internal void Send()
        {
            myObjectContext.Chats.Add(new Chat { Date = DateTime.Now, Name = this.Name, Message = this.Message });
            myObjectContext.SaveChanges();
            this.Message = null;
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        { 
            if (PropertyChanged != null) 
            { 
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
