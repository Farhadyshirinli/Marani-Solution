using Marani.Domain.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marani.Domain.Models.DataContexts.Configurations
{
    internal class BlogPostTagItemEntityTypeConfiguration : IEntityTypeConfiguration<BlogPostTagItem>
    {
        public void Configure(EntityTypeBuilder<BlogPostTagItem> builder)
        {
            builder.HasKey(k => new { k.BlogPostId, k.TagId });
            builder.Property(p => p.Id)
                .UseIdentityColumn();
            builder.ToTable("BlogPostTagCloud");
        }
    }
}
