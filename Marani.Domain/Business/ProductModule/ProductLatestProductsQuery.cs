using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.ProductModule
{
    
    public class ProductLatestProductsQuery : IRequest<List<Product>>
    {

        public int Size { get; set; }
        public int ProductId { get; set; }

        public class ProductLatestProductsQueryHandler : IRequestHandler<ProductLatestProductsQuery, List<Product>>
        {
            private readonly MaraniDbContext db;

            public ProductLatestProductsQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Product>> Handle(ProductLatestProductsQuery request, CancellationToken cancellationToken)
            {
                var posts = await db.Products
                    .Include(p => p.ProductImages.Where(i => i.DeletedDate == null))
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                     .Where(bp => bp.DeletedDate == null && bp.CreatedDate != null)
                     .OrderByDescending(bp => bp.CreatedDate)
                     .Take(request.Size < 8 ? 8 : request.Size)
                     .ToListAsync(cancellationToken);

                return posts;
            }
        }

    }
}
