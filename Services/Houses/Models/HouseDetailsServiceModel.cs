namespace HouseRentingSystem.Services.Houses.Models
{
    public class HouseDetailsServiceModel:HouseServiceModel
    {
        public string Description { get; set; }

        public string Category { get; set; }

        public AgentServiceModel Agent { get; set; }
    }
}
