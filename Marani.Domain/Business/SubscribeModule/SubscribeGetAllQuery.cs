
using MediatR;
using Microsoft.EntityFrameworkCore;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.SubscribeModule
{

    public class SubscribeGetAllQuery : IRequest<List<Subscribe>>
    {
        public class SubscribeGetAllQueryHandler : IRequestHandler<SubscribeGetAllQuery, List<Subscribe>>
        {
            private readonly MaraniDbContext db;

            public SubscribeGetAllQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Subscribe>> Handle(SubscribeGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Subscribes
                .Where(bp => bp.DeletedDate == null)
                .ToListAsync(cancellationToken);

                return data;
            }
        }


    }
}