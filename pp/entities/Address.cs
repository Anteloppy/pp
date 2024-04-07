using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    internal class Address
    {
        public int id_address { get; set; }
        public string fk_city_name { get; set; }
        public string fk_street_name { get; set; }
        public string house_number { get; set; }
        public int flat_number { get; set; }
        public override string ToString()
        {
            return "г. " + fk_city_name + ", " + fk_street_name + ", д. " + house_number + ", кв. " + flat_number;
        }
    }
}
