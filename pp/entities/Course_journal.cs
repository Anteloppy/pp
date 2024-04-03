using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    internal class Course_journal
    {
        public int id_course_journal { get; set; }
        public string fk_course_name { get; set; }
        public string fk_person { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string result { get; set; }
        public string status { get; set; }
        public string notes { get; set; }
        public string certificate_number { get; set; }
        public string certificate_date { get; set; }
    }
}
