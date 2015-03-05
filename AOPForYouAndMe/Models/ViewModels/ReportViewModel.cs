using System.ComponentModel.DataAnnotations;
using AOPForYouAndMe.Part1.Scattered.Models.Api;

namespace AOPForYouAndMe.Part1.Scattered.Models.ViewModels
{
    public class ReportViewModel
    {
        [Required]
        [Display(Name = "Group Id #")]        
        public int? GroupId { get; set; }
        [Required]
        [Display(Name = "Include forums?")]
        public bool? AreForumsIncluded { get; set; }

        public ReportResults Results { get; set; }
    }
}