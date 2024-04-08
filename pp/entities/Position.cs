using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pp.entities
{
    class Position
    {
        public int id_position { get; set; }
        public string position_name { get; set; }
        public override string ToString()
        {
            return position_name;
        }
    }
}
