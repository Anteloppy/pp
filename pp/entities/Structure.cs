using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pp.entities
{
    class Structure
    {
        public int id_structure { get; set; }
        public string fk_department_name { get; set; }
        public string fk_position_name { get; set; }
        public decimal salary { get; set; }
        public decimal bonus { get; set; }
        public override string ToString()
        {
            return "подразделение " + fk_department_name + ", должность " + fk_position_name + ", зарплата " + salary + ", премия " + bonus;
        }
    }
}
