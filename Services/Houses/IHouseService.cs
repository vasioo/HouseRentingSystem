using HouseRentingSystem.Models;
using HouseRentingSystem.Services.Houses.Models;
using HouseRentingSystem.Services.Models;

namespace HouseRentingSystem.Services.Houses
{
    public interface IHouseService
    {
        void Leave(int houseId);

        bool IsRented(int id);

        bool IsRentedByUserWithId(int houseId,string userId);

        void Rent(int houseId,string userId);

        void Delete(int houseId);

        bool HasAgentWithId(int houseId,string currentUserId);

        int GetHouseCategoryId(int houseId);

        void Edit(int houseId, string title, string address,string description
            ,string imageUrl, decimal price, int categoryId);

        bool Exists(int id);

        HouseDetailsServiceModel HouseDetailsById(int id);

        IEnumerable<HouseServiceModel> AllHousesByAgentId(int agentId);
        IEnumerable<HouseServiceModel> AllHousesByUserId(string userId);

        HouseQueryServiceModel All(
            string category = null,
            string searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1
            );

        bool CategoryExists(int categoryId);

        int Create(string title,
            string address, string description,
            string imageUrl, decimal price,
            int categoryId, int agentId);
        IEnumerable<HouseIndexServiceModel> LastThreeHouses();

        IEnumerable<HouseCategoryServiceModel> AllCategories();
        IEnumerable<string> AllCategoriesNames();
    }
}
