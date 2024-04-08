using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pp.entities
{
    internal class Region
    {
        public int id_region { get; set; }
        public string region_name { get; set; }
        public override string ToString()
        {
            return region_name;
        }
    }
}
