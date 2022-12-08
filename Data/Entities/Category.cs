using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants.Category;
namespace HouseRentingSystem.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }   
        
        public IEnumerable<House> Houses { get; set; } = new List<House>();
        
    }
}
