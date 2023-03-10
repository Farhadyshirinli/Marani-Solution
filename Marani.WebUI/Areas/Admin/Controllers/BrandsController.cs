using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Marani.Domain.Business.BrandModule;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;


namespace Marani.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly MaraniDbContext db;

        public BrandsController(MaraniDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/Colors
        public async Task<IActionResult> Index()
        {
            return View(await db.Brands.ToListAsync());
        }

        // GET: Admin/Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await db.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Add(brand);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Admin/Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await db.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Admin/Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(brand);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }


        // POST: Admin/Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await db.Brands.FindAsync(id);
            db.Brands.Remove(brand);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return db.Brands.Any(e => e.Id == id);
        }
    }
}




////------------------------------------//
//namespace Marani.WebUI.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class BrandsController : Controller
//    {
//        private readonly MaraniDbContext db;
//        private readonly IMediator mediator;

//        public BrandsController(MaraniDbContext db, IMediator mediator)
//        {
//            this.db = db;
//            this.mediator = mediator;
//        }

//        [Authorize(Policy = "admin.brands.index")]
//        public async Task<IActionResult> Index(BrandGetAllQuery query)
//        {
//            var response = await mediator.Send(query);

//            if (response == null)
//            {
//                return NotFound();
//            }

//            return View(response);
//        }

//        [Authorize(Policy = "admin.brands.details")]
//        public async Task<IActionResult> Details(BrandSingleQuery query)
//        {
//            var response = await mediator.Send(query);

//            if (response == null)
//            {
//                return NotFound();
//            }

//            return View(response);
//        }

//        //[Authorize(Policy = "admin.brands.create")]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        //[Authorize(Policy = "admin.brands.create")]
//        public async Task<IActionResult> Create(BrandCreateCommand command)
//        {
//            if (ModelState.IsValid)
//            {
//                var response = await mediator.Send(command);

//                if (response == null)
//                {
//                    return NotFound();
//                }

//                return RedirectToAction(nameof(Index));
//            }

//            return View(command);
//        }

//        //[Authorize(Policy = "admin.brands.edit")]
//        public async Task<IActionResult> Edit(int? id, BrandEditCommand command)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var entity = await db.Brands
//                .FirstOrDefaultAsync(c => c.Id == id);


//            if (entity == null)
//            {
//                return NotFound();
//            }


//            command.Id = entity.Id;
//            command.Name = entity.Name;

//            return View(command);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        //[Authorize(Policy = "admin.brands.edit")]
//        public async Task<IActionResult> Edit(BrandEditCommand command)
//        {
//            if (ModelState.IsValid)
//            {
//                var response = await mediator.Send(command);

//                if (response == null)
//                {
//                    return NotFound();
//                }

//                return RedirectToAction(nameof(Index));
//            }

//            return View(command);

//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        //[Authorize(Policy = "admin.brands.remove")]
//        public async Task<IActionResult> DeleteConfirmed(BrandRemoveCommand command)
//        {
//            var response = await mediator.Send(command);

//            if (response == null)
//            {
//                return NotFound();
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        private bool BrandExists(int id)
//        {
//            return db.Brands.Any(e => e.Id == id);
//        }
//    }
//}
