using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    internal class Personel_reserve
    {
        public int id_personel_reserve { get; set; }
        public string fk_person { get; set; }
        public string fk_structure { get; set; }
        public string reserve_entry_date { get; set; }
        public string reserve_exit_date { get; set; }
        public string reserve_status { get; set; }
        public string fk_new_structure { get; set; }
        public string fk_work_shedule_name { get; set; }
    }
}
