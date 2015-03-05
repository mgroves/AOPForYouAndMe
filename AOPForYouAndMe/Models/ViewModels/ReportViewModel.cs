using System.ComponentModel.DataAnnotations;
using AOPForYouAndMe.Models.Api;

namespace AOPForYouAndMe.Models.ViewModels
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