using System;
using System.Collections.Generic;

namespace PaccarAPI.Models
{
    public partial class Company
    {
        public Company()
        {
            BestPracticeCompany = new HashSet<BestPracticeCompany>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BestPracticeCompany> BestPracticeCompany { get; set; }
    }
}
