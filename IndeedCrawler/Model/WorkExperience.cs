using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndeedCrawler.Model
{
    public class WorkExperience
    {
        public string company { get; set; }
        public bool current { get; set; }
        public string customizedDateRange { get; set; }
        public string description { get; set; }
        public string endDate { get; set; }
        public string location { get; set; }
        public string startDate { get; set; }
        public string title { get; set; }
    }
}
