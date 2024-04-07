using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pp.entities
{
    internal class Bank
    {
        public int id_bank { get; set; }
        public string bank_name { get; set; }
        public override string ToString()
        {
            return bank_name;
        }
    }
}
