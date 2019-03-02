using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaccarAPI.Models
{
    public class BestPractice
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Summary { get; set; }
        public string Department { get; set; }
        public string Practice { get; set; }
        public string PN { get; set; }
        [NotMapped]
        public string Company { get; set; }
        [NotMapped]
        public IEnumerable<BestPracticeCompany> BestPracticeCompanies {get; set;}
    }
}