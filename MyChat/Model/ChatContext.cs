// Copyright © 2012 by blueshell Software Engineering Harry von Borstel (http://www.blueshell.com)
// This work is licensed under COPL (see http://www.codeproject.com/info/cpol10.aspx)
// 
// 
using System.Data.Entity;

namespace MyChat.Model
{
    public class ChatContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
    }
}
