using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Services.Houses.Models
{
    public class HouseServiceModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Address { get; set; }

        [DisplayName("ImageURL")]
        public string? ImageUrl { get; set; }

        [DisplayName("Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [DisplayName("Is Rented")]
        public bool IsRented { get; set; }
    }
}
