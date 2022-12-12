using HouseRentingSystem.Models.Services;
using HouseRentingSystem.Services.Houses.Models;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Models.Houses
{
    public class AllHousesQueryModel
    {
        public const int HousesPerPage = 3;

        public string? Category { get; set; }

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; set; }

        public HouseSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        public int TotalHousesCount { get; set; }

        public IEnumerable<string>? Categories { get; set; }

        public IEnumerable<HouseServiceModel> Houses { get; set; }
        = new List<HouseServiceModel>();
    }
}
