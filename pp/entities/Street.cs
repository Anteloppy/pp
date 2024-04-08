using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pp.entities
{
    internal class Street
    {
        public int id_street { get; set; }
        public string street_name { get; set; }
        public override string ToString()
        {
            return street_name;
        }
    }
}
