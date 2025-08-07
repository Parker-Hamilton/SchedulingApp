using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleingApp
{
    internal class Credentials

    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        private static Credentials _instance;
        public Credentials(string username = "", string password = "")
        {
            Username = username;
            Password = password;
        }
        public static void Init() { 
            if(_instance == null)
            {
                _instance = new Credentials();
            }
        }
        public void Set(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public void Clear()
        {
            Username = null;
            Password = null;
        }
        internal static Credentials Instance => _instance;
    }
}

