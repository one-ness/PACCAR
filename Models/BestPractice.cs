using System;
using System.Collections.Generic;

namespace PaccarAPI.Models
{
    public partial class BestPractice
    {
        public BestPractice()
        {
            BestPracticeCompany = new HashSet<BestPracticeCompany>();
        }

        public int BestPracticeId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Summary { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string Practice { get; set; }
        public string Pn { get; set; }

        public virtual ICollection<BestPracticeCompany> BestPracticeCompany { get; set; }
    }
}
