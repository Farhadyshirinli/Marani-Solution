using Marani.Domain.AppCode.Infrastructure;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.ProductModule
{
    public class ProductsPagedQuery :PaginateModel, IRequest<PagedViewModel<Product>>
    {
        public class ProductsPagedQueryHandler : IRequestHandler<ProductsPagedQuery, PagedViewModel<Product>>
        {
            private readonly MaraniDbContext db;

            public ProductsPagedQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }

            public async Task<PagedViewModel<Product>> Handle(ProductsPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Products
                    .Include(p => p.ProductImages.Where(i => i.DeletedDate == null))
                    .Include(p=>p.Brand)
                    .Include(p=>p.Category)
                    
                
                    .Where(m => m.DeletedDate == null)
                    .OrderByDescending(p=>p.Id)
                    .AsQueryable();

                var pagedDate = new PagedViewModel<Product>(query, request);

                return pagedDate;
            }
        }
    }
}
