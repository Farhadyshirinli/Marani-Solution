using Marani.Domain.AppCode.Extensions;
using Marani.Domain.AppCode.Infrastructure;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.BasketModule
{
    public class AddToBasketCommand : IRequest<JsonResponse>
    {
        public int ProductId { get; set; }

        public class AddToBasketCommandHandler : IRequestHandler<AddToBasketCommand, JsonResponse>
        {
            private readonly MaraniDbContext db;
            private readonly IActionContextAccessor ctx;

            public AddToBasketCommandHandler(MaraniDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<JsonResponse> Handle(AddToBasketCommand request, CancellationToken cancellationToken)
            {
                var userId = ctx.GetCurrentUserId();

                var alreadyExits = await db.Basket.AnyAsync(b => b.ProductId == request.ProductId && b.UserId == userId, cancellationToken);


                if (alreadyExits)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Product already exist in your basket"
                    };
                }


                var basketItem = new Basket
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Quantity = 1
                };

                await db.Basket.AddAsync(basketItem, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);



                var info = await (from b in db.Basket
                                  join p in db.Products on b.ProductId equals p.Id
                                  where b.UserId == userId
                                  select new
                                  {
                                      b.UserId,
                                      SubTotal = p.Price * b.Quantity
                                  })
                                  .GroupBy(g => g.UserId)
                                  .Select(g => new
                                  {
                                      Total = g.Sum(m => m.SubTotal),
                                      Count = g.Count()
                                  })
                                  .FirstOrDefaultAsync(cancellationToken);

                var addedProduct = await db.Basket
                    .Include(b => b.Product)
                    .ThenInclude(p => p.ProductImages.Where(i => i.IsMain == true))
                    .FirstOrDefaultAsync(b => b.UserId == userId && b.ProductId == request.ProductId);


                return new JsonResponse
                {
                    Error = false,
                    Message = "Product was added to the basket",
                    Value = new
                    {
                        Product = addedProduct,
                        BasketInfo = info
                    }
                };
            }


        }
    }
}
