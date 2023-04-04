using Marani.Domain.AppCode.Extensions;
using Marani.Domain.AppCode.Infrastructure;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.ContactPostModule
{
    
        public class ContactPostPostCommand : IRequest<JsonResponse>
        {
            public string Location { get; set; }

            public string PhoneNumber { get; set; }

            public string CompanyEmail { get; set; }

            public class ContactPostPostCommandHandler : IRequestHandler<ContactPostPostCommand, JsonResponse>
            {
                private readonly MaraniDbContext db;
                private readonly IHostEnvironment env;

                public ContactPostPostCommandHandler(MaraniDbContext db, IHostEnvironment env)
                {
                    this.db = db;
                    this.env = env;
                }
                public async Task<JsonResponse> Handle(ContactPostPostCommand request, CancellationToken cancellationToken)
                {
                    var entity = new ContactInfo();

                    entity.Location = request.Location;
                    entity.PhoneNumber = request.PhoneNumber;
                    entity.CompanyEmail = request.CompanyEmail;

                   


              

                    await db.ContactInfos.AddAsync(entity, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return new JsonResponse
                    {
                        Error = false,
                        Message = "Success"
                    };
                }
            }
        }
    
}
