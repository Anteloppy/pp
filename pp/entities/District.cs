using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    internal class District
    {
        public int id_district { get; set; }
        public string district_name { get; set; }
        public string fk_region_name { get; set; }
    }
}
