using Marani.Domain.AppCode.Infrastructure;
using System.Collections.Generic;

namespace Marani.Domain.Models.Entities
{
    public class ProductColor : BaseEntity
    {
        public string Taste { get; set; }
        public virtual ICollection<ProductCatalogItem> ProductCatalog { get; set; }
    }
}
