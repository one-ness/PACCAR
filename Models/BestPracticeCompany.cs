using System.ComponentModel.DataAnnotations.Schema;
namespace PaccarAPI.Models
{
    public class BestPracticeCompany
    {
        public int BestPracticeId {get; set;}

        public int CompanyId {get; set;}

        public BestPractice BestPractice { get; set; }

        public Company Company { get; set; }
    }
}