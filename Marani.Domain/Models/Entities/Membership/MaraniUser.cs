using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Marani.Domain.Models.Entities.Membership
{
    public class MaraniUser:IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Surname { get; set; }
    }
}
