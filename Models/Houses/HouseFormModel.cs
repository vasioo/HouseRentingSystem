using HouseRentingSystem.Services.Houses;
using HouseRentingSystem.Services.Houses.Models;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants.House;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using StringLengthAttribute = ServiceStack.DataAnnotations.StringLengthAttribute;

namespace HouseRentingSystem.Models.Houses
{
    public class HouseFormModel:IHouseModel
    {
        [Required]
        [StringLength(TitleMaxLength,MinimumLength = TitleMinLength)]
        public string? Title { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string? Address { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string? Description { get; set; }

        [Required]
        [Display(Name ="Image URL")]
        public string? ImageUrl { get; set; }

        [Required]
        [Range(0.00,2000,
            ErrorMessage = "Price per month must be a positive number and less than {2} BGN")]
        [Display(Name ="Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; }
        = new List<HouseCategoryServiceModel>();
    }
}
