using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ScheduleingApp
{
    public class StoredProcedureParameters
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public MySqlDbType MySqlDbType { get; set; }
        public StoredProcedureParameters(string name, object value, MySqlDbType mySqlDbType) 
        {
            this.Name = name;
            this.Value = value;
            this.MySqlDbType = mySqlDbType;
        }
    }
}
