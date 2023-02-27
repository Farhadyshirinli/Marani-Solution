using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entites;
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
    public class ProductSingleQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public class ProductSingleQueryHandler : IRequestHandler<ProductSingleQuery, Product>
        {
            private readonly MaraniDbContext db;

            public ProductSingleQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }
            public async Task<Product> Handle(ProductSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Products
                    .Include(p=>p.ProductImages.Where(i=>i.DeletedDate == null))
                    .Include(p=>p.Brand)
                    .Include(p=>p.Category)
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null,cancellationToken);

                return data;
            }
        }
    }
}
