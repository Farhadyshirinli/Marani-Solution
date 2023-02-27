using Marani.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marani.Domain.Models.DataContexts.Configurations
{
    public class ProductCatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<ProductCatalogItem>
    {
        public void Configure(EntityTypeBuilder<ProductCatalogItem> builder)
        {
            builder.HasKey(k => new {
                k.ProductId,
                k.ProductRegionId,
                k.ProductColorId
            });

            builder.Property(t => t.Id).UseIdentityColumn();
            builder.HasIndex(t => t.Id).IsUnique();
            builder.ToTable("ProductCatalog");
        }
    }
}
