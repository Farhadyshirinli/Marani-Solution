using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.ContactPostModule
{

    public class ContactPostGetAllQuery : IRequest<List<ContactPost>>
    {
        public class ContactPostGetAllQueryHandler : IRequestHandler<ContactPostGetAllQuery, List<ContactPost>>
        {
            private readonly MaraniDbContext db;

            public ContactPostGetAllQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }
            public async Task<List<ContactPost>> Handle(ContactPostGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ContactPosts
                .Where(bp => bp.DeletedDate == null)
                .ToListAsync(cancellationToken);

                return data;
            }
        }


    }
}
