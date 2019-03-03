using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaccarAPI.Models
{
    public class Company
    {
        [Key]
        public int CompanyId {get; set;}
        public string Name {get; set;}
        
        public ICollection<BestPracticeCompany> CompanyBestPractices {get;set;}
    }
}