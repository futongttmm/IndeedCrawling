using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndeedCrawler.Model
{
    public class User
    {
        public string accountKey { get; set; }
        public string additionalInfo { get; set; }
        public bool anonymous { get; set; }
        public string city { get; set; }
        public bool confirmed { get; set; }
        public string displayFirstName { get; set; }
        public string displayFullName { get; set; }
        public IList<Education> educations { get; set; }
        public string firstName { get; set; }
        public string headline { get; set; }
        public string lastName { get; set; }
        public string lastUpdated { get; set; }
        public string skills { get; set; }
        public IList<object> skillsList { get; set; }
        public string url { get; set; }
        public IList<WorkExperience> workExperiences { get; set; }
    }
}
