// Copyright © 2012 by blueshell Software Engineering Harry von Borstel (http://www.blueshell.com)
// This work is licensed under COPL (see http://www.codeproject.com/info/cpol10.aspx)
// 
// 
using MyChat.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ChatContext MyContext;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                MyContext = new ChatContext();
                ((System.Data.Entity.Infrastructure.IObjectContextAdapter)MyContext).ObjectContext.Connection.Open();
                base.OnStartup(e);
                new MainWindow().Show();
            }
            catch (EntityException ex)
            {
                MessageBox.Show(
                    string.Format("{0}\n\n{1}\n\nPlease make sure that you can logon to the SQL Server \"{2}\" or edit the file \"MyChat.exe.config\".", ex.Message, ex.InnerException != null ? ex.InnerException.Message : null, MyContext.Database.Connection.DataSource),
                    "MyChat", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
        }

        public new static App Current
        {
            get { return (Application.Current as App); }
        }
    }
}

