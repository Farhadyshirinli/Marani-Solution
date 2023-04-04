using Marani.Domain.Models.Entities.Membership;

namespace Marani.Domain.Models.Entities
{
    public class Basket
    {
        public int UserId { get; set; }
        public virtual MaraniUser User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
