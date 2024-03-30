using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp.entities
{
    internal class Work_schedule
    {
        public int id_work_schedule { get; set; }
        public string schedule_name { get; set; }
        public int working_days { get; set; }
        public int days_off { get; set; }
        public int working_hours_per_day { get; set; }
    }
}
