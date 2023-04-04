using Marani.Domain.AppCode.Infrastructure;

namespace Marani.Domain.Models.Entities
{
    public class BlogPostTagItem : BaseEntity
    {
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }

        public int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }
    }
}
