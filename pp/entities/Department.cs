using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    class Department
    {
        public int id_department{ get; set; }
        public string department_name { get; set; }
        public override string ToString()
        {
            return department_name;
        }
    }
}
