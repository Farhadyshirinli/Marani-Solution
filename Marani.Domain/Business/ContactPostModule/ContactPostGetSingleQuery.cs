using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.ContactPostModule
{
    public class ContactPostGetSingleQuery : IRequest<ContactPost>
    {
        public int Id { get; set; }
        public class ContactPostGetSingleQueryHandler : IRequestHandler<ContactPostGetSingleQuery, ContactPost>
        {
            private readonly MaraniDbContext db;

            public ContactPostGetSingleQueryHandler(MaraniDbContext db)
            {
                this.db = db;
            }

            public async Task<ContactPost> Handle(ContactPostGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ContactPosts.FirstOrDefaultAsync(p => p.Id == request.Id);

                return data;
            }
        }

    }
}
