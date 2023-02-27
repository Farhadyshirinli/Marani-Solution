using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.CategoryModule
{
    public class CategoryCreateCommand : IRequest<Category>
    {
        [Required]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, Category>
        {
            private readonly MaraniDbContext db;

            public CategoryCreateCommandHandler(MaraniDbContext db)
            {
                this.db = db;
            }

            public async Task<Category> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
            {
                var entity = new Category();


                entity.Name = request.Name;
                entity.ParentId = request.ParentId;
                

                await db.Categories.AddAsync(entity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }


        }
    }

}
