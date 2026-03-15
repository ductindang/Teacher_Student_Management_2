using BLL.DTOs.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class CreateScheduleRequest
    {
        public int ClassId { get; set; }

        public EDateOfWeek DaysOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Room { get; set; }
    }
}
