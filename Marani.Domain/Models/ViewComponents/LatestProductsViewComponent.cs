using Marani.Domain.Business.ProductModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marani.Domain.Models.ViewComponents
{
    public class LatestProductsViewComponent: ViewComponent
    {
        private readonly IMediator mediator;

        public LatestProductsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = new ProductLatestProductsQuery() { Size = 8 };

            var posts = await mediator.Send(query);

            return View(posts);
        }
    }
}
