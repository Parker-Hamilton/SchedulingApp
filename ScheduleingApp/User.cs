using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleingApp
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public void SetDetailsFromDatabase(int id,  string username)
        {
            this.Id = id;
            this.Username = username;
        }
    }
}
