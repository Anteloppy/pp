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
        public string birht_date { get; set; }
        public string region { get; set; }
        public string district { get; set; }
        public string settlement { get; set; }
        public string street { get; set; }
        public string house_number { get; set; }
        public string flat_number { get; set; }
        public string bank { get; set; }
        public string bank_account { get; set; }
        public string INN { get; set; }
        public string SNILS { get; set; }
        public string employment_date { get; set; }
        public string dismissal_date { get; set; }
        public string work_schedule { get; set; }
    }
}
