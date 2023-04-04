using Marani.Domain.AppCode.Extensions;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.ProductModule
{
    public class ProductBasketQuery : IRequest<IEnumerable<Basket>>
    {
        public class ProductBasketQueryHandler : IRequestHandler<ProductBasketQuery, IEnumerable<Basket>>
        {
            private readonly MaraniDbContext db;
            private readonly IActionContextAccessor ctx;

            public ProductBasketQueryHandler(MaraniDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<IEnumerable<Basket>> Handle(ProductBasketQuery request, CancellationToken cancellationToken)
            {
                if (!ctx.ActionContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    return Enumerable.Empty<Basket>();
                }

                var userId = ctx.GetCurrentUserId();

                var data = await db.Basket
                    .Include(b => b.Product)
                    .ThenInclude(p => p.ProductImages.Where(i => i.IsMain == true && i.DeletedByUserId == null))
                    .Where(b=>b.Product.DeletedDate==null)
                    .Where(b => b.UserId == userId)
                    .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}