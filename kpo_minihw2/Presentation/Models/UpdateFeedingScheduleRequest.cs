using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpo_minihw2.Presentation.Models
{
    public class UpdateFeedingScheduleRequest
    {
        public DateTime NewFeedingTime { get; set; }
        public string FoodTypeName { get; set; }
    }
}
