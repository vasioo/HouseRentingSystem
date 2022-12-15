using HouseRentingSystem.Services.Houses.Models;

namespace HouseRentingSystem.Web.Areas.Admin.Models
{
    public class MyHouseViewModel
    {
        public IEnumerable<HouseServiceModel> AddedHouses { get; set; }
            = new List<HouseServiceModel>();
        public IEnumerable<HouseServiceModel> RentedHouses { get; set; }
            = new List<HouseServiceModel>();
    }
}
