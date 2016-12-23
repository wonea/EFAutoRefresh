// Copyright © 2012 by blueshell Software Engineering Harry von Borstel (http://www.blueshell.com)
// This work is licensed under COPL (see http://www.codeproject.com/info/cpol10.aspx)
// 
// 
using MyChat.Model;
using System.Data.Entity.Core;
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
                ((System.Data.Entity.Infrastructure.IObjectContextAdapter) MyContext).ObjectContext.Connection.Open();
                base.OnStartup(e);
                new MainWindow().Show();
            }
            catch (EntityException ex)
            {
                MessageBox.Show(
                    $"{ex.Message}\n\n{ex.InnerException?.Message}\n\nPlease make sure that you can logon to the SQL Server \"{MyContext.Database.Connection.DataSource}\" or edit the file \"MyChat.exe.config\".",
                    "MyChat", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
            }
        }

        public new static App Current => Application.Current as App;
    }
}