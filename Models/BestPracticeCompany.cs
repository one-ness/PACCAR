using System;
using System.Collections.Generic;

namespace PaccarAPI.Models
{
    public partial class BestPracticeCompany
    {
        public int BestPracticeId { get; set; }
        public int CompanyId { get; set; }

        public virtual BestPractice BestPractice { get; set; }
        public virtual Company Company { get; set; }
    }
}
