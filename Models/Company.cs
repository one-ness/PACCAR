using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaccarAPI.Models
{
    public class Company
    {
        [Key]
        public int Id {get; set;}
        public string Name {get; set;}
        [NotMapped]
        public IEnumerable<BestPracticeCompany> CompanyBestPractices {get;set;}
    }
}