using Marani.Domain.AppCode.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marani.Domain.Models.Entities
{
    public class Product : BaseEntity, IPageable
    {
        public string Name { get; set; }

        public string StockKeepingUnit { get; set; }

        public decimal Price { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImages> ProductImages { get; set; }

        public virtual ICollection<ProductCatalogItem> ProductCatalog { get; set; }
    }
}
