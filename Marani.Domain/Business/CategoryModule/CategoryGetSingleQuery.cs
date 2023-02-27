using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.CategoryModule
{
    public class CategoryGetSingleQuery : IRequest<Category>
    {
        public int Id { get; set; }

        public class CategoryGetSingleQueryHandler : IRequestHandler<CategoryGetSingleQuery, Category>
        {
            private readonly MaraniDbContext db;

            public CategoryGetSingleQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }

            public async Task<Category> Handle(CategoryGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Categories
                    .Include(c => c.Parent)
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                return data;
            }
        }

    }
}
