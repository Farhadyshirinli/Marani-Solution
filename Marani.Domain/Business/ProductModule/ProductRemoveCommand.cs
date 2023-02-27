using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.ProductModule
{
    public class ProductRemoveCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
        {
            private readonly MaraniDbContext db;

            public ProductRemoveCommandHandler(MaraniDbContext db)
            {
                this.db = db;
            }
            public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Products
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);

                if (data == null)
                {
                    return null;
                }

                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);


                return data;
            }
        }
    }
}
