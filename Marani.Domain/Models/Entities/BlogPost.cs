using Marani.Domain.AppCode.Infrastructure;
using Marani.Domain.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marani.Domain.Models.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public DateTime? PublishedDate { get; set; }
        public virtual ICollection<BlogPostComment> Comments { get; set; }
        public int? AuthorId { get; set; }
        public virtual ICollection<BlogPostTagItem> TagCloud { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
