using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    internal class City
    {
        public int id_city { get; set; }
        public string city_name { get; set; }
        public string fk_district_name { get; set; }
        public override string ToString()
        {
            return "район " + fk_district_name + ", г. " + city_name;
        }
    }
}
