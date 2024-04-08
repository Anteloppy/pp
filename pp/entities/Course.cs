using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pp.entities
{
    internal class Course
    {
        public int id_course { get; set; }
        public string course_name { get; set; }
        public override string ToString()
        {
            return course_name;
        }
    }
}
