using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    internal class Contact_info
    {
        public int id_contact_info { get; set; }
        public string fk_person { get; set; }
        public string fk_position_name { get; set; }
        public string fk_department_name { get; set; }
        public string mobile_phone { get; set; }
        public string landline_phone { get; set; }
        public string email { get; set; }
        public string fk_supervisor_person { get; set; }
    }
}
