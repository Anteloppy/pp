using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pp.entities
{
    internal class Work_schedule
    {
        public int id_work_schedule { get; set; }
        public string schedule_name { get; set; }
        public int working_days { get; set; }
        public int days_off { get; set; }
        public int working_hours_per_day { get; set; }
        public override string ToString()
        {
            return "рассписание " + schedule_name + ", рабочих дней " + working_days + ", нерабочих дней " + days_off + ", рабочих часов в день " + working_hours_per_day;
        }
    }
}
