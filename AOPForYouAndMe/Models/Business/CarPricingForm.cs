using System.ComponentModel.DataAnnotations;

namespace AOPForYouAndMe.Models.Business
{
    public class CarPricingForm
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Year { get; set; }
    }
}