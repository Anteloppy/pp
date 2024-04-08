using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pp.entities
{
    internal class District
    {
        public int id_district { get; set; }
        public string district_name { get; set; }
        public string fk_region_name { get; set; }
        public override string ToString()
        {
            return  "регион " + fk_region_name + ", район " + district_name;
        }
    }
}
