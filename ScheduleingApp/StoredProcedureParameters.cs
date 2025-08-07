using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ScheduleingApp
{
    public class StoredProcedureParameters
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public SqlDbType SqlDbType { get; set; }
        public StoredProcedureParameters(string name, object value, SqlDbType sqlDbType) 
        {
            this.Name = name;
            this.Value = value;
            this.SqlDbType = sqlDbType;
        }
    }
}
