using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Entities;
using HouseRentingSystem.Models;
using HouseRentingSystem.Services.Houses.Models;
using HouseRentingSystem.Services.Models;

namespace HouseRentingSystem.Services.Houses
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext data;

        public HouseService(HouseRentingDbContext _data)
        {
            data = _data;
        }

        public HouseQueryServiceModel All(string category = null!
            , string searchTerm = null!
            , HouseSorting sorting = HouseSorting.Newest
            , int currentPage = 1, int housesPerPage = 1)
        {
            var housesQuery = this.data.Houses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                housesQuery = this.data
                    .Houses
                    .Where(h => h.Category!.Name == category);
            }
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                housesQuery = housesQuery.Where(h =>
                h.Title!.ToLower().Contains(searchTerm.ToLower()) ||
                h.Address!.ToLower().Contains(searchTerm.ToLower()) ||
                h.Description!.ToLower().Contains(searchTerm.ToLower()));
            }
            housesQuery = sorting switch
            {
                HouseSorting.Price => housesQuery
                .OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery
                .OrderBy(h => h.RenterId != null)
                .ThenByDescending(h => h.Id),
                _ => housesQuery.OrderByDescending(h => h.Id)
            };
            var houses = housesQuery
                .Skip((currentPage) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title!,
                    Address = h.Address!,
                    ImageUrl = h.ImageUrl!,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth
                }).ToList();

            var totalHours = housesQuery.Count();

            return new HouseQueryServiceModel()
            {
                TotalHousesCount = totalHours,
                Houses = houses
            };
        }

        public IEnumerable<HouseCategoryServiceModel> AllCategories()
        {
            return this.data.Categories.Select(c => new HouseCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name!
            }).ToList();
        }

        public IEnumerable<string> AllCategoriesNames()
        => this.data
            .Categories
            .Select(c => c.Name!)
                .Distinct()
                .ToList();

        public IEnumerable<HouseServiceModel> AllHousesByAgentId(int agentId)
        {
            var houses = this.data
                 .Houses
                 .Where(h => h.AgentId == agentId)
                 .ToList();

            return ProjectToModel(houses);
        }

        public IEnumerable<HouseServiceModel> AllHousesByUserId(string userId)
        {
            var houses = this.data
                .Houses
                .Where(h => h.RenterId == userId)
                .ToList();

            return ProjectToModel(houses);
        }

        public List<HouseServiceModel> ProjectToModel(List<House> houses)
        {
            var resultHouses = houses
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title!,
                    Address = h.Address!,
                    ImageUrl = h.ImageUrl!,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null
                }).ToList();
            return resultHouses;
        }

        public bool CategoryExists(int categoryId)
        {
            return this.data.Categories.Any(c => c.Id == categoryId);
        }

        public int Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, int agentId)
        {
            var house = new House
            {
                Title = title,
                Address = address,
                Description = description,
                ImageUrl = imageUrl,
                PricePerMonth = price,
                CategoryId = categoryId,
                AgentId = agentId
            };
            this.data.Houses.Add(house);
            this.data.SaveChanges();

            return house.Id;
        }

        public IEnumerable<HouseIndexServiceModel> LastThreeHouses()
        {
            return data.Houses.OrderByDescending(c => c.Id)
                .Select(c => new HouseIndexServiceModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImageUrl = c.ImageUrl,
                })
                .Take(3);
        }

        public bool Exists(int id)
        {
            return this.data.Houses.Any(h => h.Id == id);
        }

        public HouseDetailsServiceModel? HouseDetailsById(int id)
            => this.data
                .Houses
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title!,
                    Address = h.Address!,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl!,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                    Category = h.Category!.Name,
                    Agent = new AgentServiceModel()
                    {
                        PhoneNumber = h.Agent!.PhoneNumber!,
                        Email = h.Agent!.User!.Email
                    }
                }).FirstOrDefault();

        public void Edit(int houseId, string title, string address,
            string description, string imageUrl, decimal price, int categoryId)
        {
            var house = this.data.Houses.Find(houseId);

            house!.Title = title;
            house!.Address = address;
            house!.Description = description;
            house!.ImageUrl = imageUrl;
            house!.PricePerMonth = price;
            house!.CategoryId = categoryId;

            this.data.SaveChanges();
        }

        public bool HasAgentWithId(int houseId, string currentUserId)
        {
            var house = this.data.Houses.Find(houseId);
            var agent = this.data.Agents.FirstOrDefault(a => a.Id == house!.AgentId);

            if (agent == null)
            {
                return false;
            }
            if (agent.UserId != currentUserId)
            {
                return false;
            }
            return true;
        }

        public int GetHouseCategoryId(int houseId)
        {
            return this.data!.Houses!.Find(houseId!).CategoryId;
        }

        public void Delete(int houseId)
        {
            var house = this.data!.Houses.Find(houseId);

            this.data!.Remove(house);
            this.data.SaveChanges();
        }

        public bool IsRented(int id)
        {
            return this.data!.Houses!.Find(id!).RenterId != null;
        }

        public bool IsRentedByUserWithId(int houseId, string userId)
        {
            var house = this.data.Houses.Find(houseId);

            if (house == null)
            {
                return false;
            }
            if (house.RenterId != userId)
            {
                return false;
            }
            return true;
        }

        public void Rent(int houseId, string userId)
        {
            var house = this.data.Houses.Find(houseId);

            house.RenterId = userId;
            this.data.SaveChanges();
        }

        public void Leave(int houseId)
        {
            var house = this.data.Houses.Find(houseId);

            house.RenterId = null;
            this.data.SaveChanges();
        }
    }
}
