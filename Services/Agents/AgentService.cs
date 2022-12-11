using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Entities;

namespace HouseRentingSystem.Services.Agents
{
    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext data;

        public AgentService(HouseRentingDbContext data)
        {
            this.data = data;
        }

        public bool UserWithPhoneNumberExists(string phoneNumber)
        {
            return this.data.Agents.Any(a => a.PhoneNumber == phoneNumber);
        }
        public bool UserHasRents(string userId)
        {
            return this.data.Houses.Any(h => h.RenterId == userId);
        }

        public void Create(string userId, string PhoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = PhoneNumber
            };
            this.data.Agents.Add(agent);
            this.data.SaveChanges();
        }

        public bool ExistsById(string userId)
        {
            return this.data.Agents.Any(a => a.UserId == userId);
        }

        public int GetAgentId(string userId)
        {
            return this!
                .data!
                .Agents!
                .FirstOrDefault(a => a.UserId == userId!).Id;
        }
    }
}
