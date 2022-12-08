using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants.Agent;
using RequiredAttribute = ServiceStack.DataAnnotations.RequiredAttribute;
using StringLengthAttribute = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace HouseRentingSystem.Data.Entities
{
    public class Agent
    {
        [Key]
        [Unique]
        public int Id { get; set; }

        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? UserId { get; set; }

        public IdentityUser? User { get; set; }
    }
}
