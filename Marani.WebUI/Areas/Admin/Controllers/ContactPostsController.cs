using Marani.Domain.AppCode.Services;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Marani.WebUI.Areas.Admin.Controllers
{
    
        [Area("Admin")]
        public class ContactPostsController : Controller
        {
            private readonly MaraniDbContext db;
            private readonly EmailService emailService;

            public ContactPostsController(MaraniDbContext db, EmailService emailService)
            {
                this.db = db;
                this.emailService = emailService;
            }

            public async Task<IActionResult> Index()
            {
                return View(await db.ContactPosts.ToListAsync());
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var contactPost = await db.ContactPosts
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (contactPost == null)
                {
                    return NotFound();
                }

                return View(contactPost);
            }
            public async Task<IActionResult> Reply(int? id)
            {
                if (id == null)
                {
                    NotFound();
                }

                var contactPost = await db.ContactPosts.FirstOrDefaultAsync(m => m.Id == id);
                if (contactPost == null)
                {
                    return NotFound();
                }

                return View(contactPost);
            }
            [HttpPost]
            public async Task<IActionResult> Reply(int id, [FromForm][Bind("Name, Email, Surname, Message, Subject, Answer, EmailAnswer")] ContactPost model)
            {
                var entity = db.ContactPosts.FirstOrDefault(bp => bp.Id == id && bp.AnswerDate == null);

                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        error = true,
                        message = "Xeta"
                    });
                }

                //entity.AnswerDate = DateTime.UtcNow.AddHours(4);
                entity.Answer = model.Answer;
                entity.EmailAnswer = model.EmailAnswer;
                await db.SaveChangesAsync();

            await emailService.SendEmailAsync(model.Email, model.EmailAnswer, model.Answer);

            return Json(new
                {
                    error = false,
                    message = "Your answer has been sended"
                });
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var contactPost = await db.ContactPosts
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (contactPost == null)
                {
                    return NotFound();
                }

                return View(contactPost);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var contactPost = await db.ContactPosts.FindAsync(id);
                db.ContactPosts.Remove(contactPost);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ContactPostExists(int id)
            {
                return db.ContactPosts.Any(e => e.Id == id);
            }
        }
    
}
