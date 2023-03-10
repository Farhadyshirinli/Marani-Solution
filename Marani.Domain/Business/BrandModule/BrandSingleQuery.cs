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

namespace Marani.Domain.Business.BrandModule
{
    public class BrandSingleQuery:IRequest<Brand>
    {
        public int Id { get; set; }

        public class BrandSingleQueryHandler : IRequestHandler<BrandSingleQuery, Brand>
        {
            private readonly MaraniDbContext db;

            public BrandSingleQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Brands
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);

                return data;
            }
        }
    }

}
