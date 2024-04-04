using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    class Personal_card
    {
        public int id_person { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birth_date { get; set; }
        public string fk_address { get; set; }
        public string fk_bank { get; set; }
        public string bank_account { get; set; }
        public string INN { get; set; }
        public string SNILS { get; set; }
        public string employment_date { get; set; }
        public string dismissal_date { get; set; }
    }
}
