using Marani.Domain.AppCode.Infrastructure;

namespace Marani.Domain.Models.Entities
{
    public class ProductCatalogItem : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int ProductColorId { get; set; }
        public virtual ProductColor ProductColor { get; set; }
        public int ProductRegionId { get; set; }
        public virtual ProductRegion ProductRegion { get; set; }
       





    }
}
