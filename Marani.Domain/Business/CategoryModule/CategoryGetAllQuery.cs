using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.CategoryModule
{
    public class CategoryGetAllQuery : IRequest<List<Category>>
    {
        public class CategoryGetAllQueryHandler : IRequestHandler<CategoryGetAllQuery, List<Category>>
        {
            private readonly MaraniDbContext db;

            public CategoryGetAllQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Category>> Handle(CategoryGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Categories
                .Where(bp => bp.DeletedDate == null)
                .ToListAsync(cancellationToken);

                return data;
            }
        }


    }
}
