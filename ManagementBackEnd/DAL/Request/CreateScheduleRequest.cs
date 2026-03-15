using DAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Request
{
    public class CreateScheduleRequest
    {
        public int ClassId { get; set; }

        public List<EDateOfWeek> DaysOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Room { get; set; }
    }
}
