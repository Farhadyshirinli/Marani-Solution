using Marani.Domain.AppCode.Extensions;
using Marani.Domain.AppCode.Infrastructure;
using Marani.Domain.Business.ProductModule;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Marani.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly MaraniDbContext db;
        private readonly IMediator mediator;

        public ProductsController(MaraniDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

   
        public async Task<IActionResult> Index(ProductsPagedQuery query)
        {
            var response = await mediator.Send(query);

            return View(response);
        }


  
        public async Task<IActionResult> Details(ProductSingleQuery query)
        {
            var response = await mediator.Send(query);

            return View(response);
        }

      
        public IActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands.Where(b => b.DeletedDate == null), "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories.Where(b => b.DeletedDate == null), "Id", "Name");
            ViewBag.TasteId = new SelectList(db.ProductColors.Where(b => b.DeletedDate == null), "Id", "Taste");
            ViewBag.RegionId = new SelectList(db.ProductRegions.Where(b => b.DeletedDate == null), "Id", "Name");

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "admin.products.create")]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            if (command.Images == null)
            {
                ModelState.AddModelError("ImagePath", "Blog image should not be left empty");
            }

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);

                return RedirectToAction(nameof(Index));
            }



                ViewBag.BrandId = new SelectList(db.Brands.Where(b => b.DeletedDate == null), "Id", "Name", command.BrandId);
                ViewBag.CategoryId = new SelectList(db.Categories.Where(b => b.DeletedDate == null), "Id", "Name", command.CategoryId);
                ViewBag.TasteId = new SelectList(db.ProductColors.Where(b => b.DeletedDate == null), "Id", "Taste");
                ViewBag.RegionId = new SelectList(db.ProductRegions.Where(b => b.DeletedDate == null), "Id", "Name");


                return View(command);

            
        }

        //[Authorize(Policy = "admin.products.edit")]
        public async Task<IActionResult> Edit(ProductSingleQuery query ,int? id)
        {
            var product1 = await mediator.Send(query);
            var product = await db.Products.FirstOrDefaultAsync(p=>p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var data = db.ProductCatalog.Where(p => p.ProductId == id).ToList();
            var command = new ProductEditCommand();
            command.Id = product.Id;
            command.Name = product.Name;
            command.Price = product.Price;
            command.ShortDescription = product.ShortDescription;
            command.Description = product.Description;
            command.BrandId = product.BrandId;
            command.CategoryId = product.CategoryId;
            command.StockKeepingUnit = product.StockKeepingUnit;
        

            command.Images = product1.ProductImages.Select(x => new ImageItem
            {
                Id = x.Id,
                TempPath = x.Name,
                IsMain = x.IsMain,


            }).ToArray();

            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
     
            return View(command);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "admin.products.edit")]
        public async Task<IActionResult> Edit(int id,ProductEditCommand command)
        {
         
           
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);

            if (response == null)
            {

                ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", command.BrandId);
                ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", command.CategoryId);
              
                return View(command);

            }

            return RedirectToAction(nameof(Index));
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "admin.products.delete")]
        public async Task<IActionResult> DeleteConfirmed(ProductRemoveCommand command, ProductsPagedQuery request)
        {


            var response = await mediator.Send(command);

            var query = db.Products
                .Where(p => p.DeletedDate == null)
                   .Include(p => p.ProductImages.Where(i => i.DeletedDate == null))
                  .Include(p => p.Brand)
                  .Include(p => p.Category)
                  .AsQueryable();

            var pagedDate = new PagedViewModel<Product>(query, request);

            return PartialView("_ListBody", pagedDate);


        }


        private bool ProductExists(int id)
        {
            return db.Products.Any(e => e.Id == id);
        }

    }
}
