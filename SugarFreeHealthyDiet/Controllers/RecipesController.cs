using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using SugarFreeHealthyDiet.Data;
using SugarFreeHealthyDiet.Models;
using SugarFreeHealthyDiet.Common;
using SugarFreeHealthyDiet.Services;
using SugarFreeHealthyDiet.Extensions;
using X.PagedList;

namespace SugarFreeHealthyDiet.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationDbContext dbContext;
        private readonly ISlugService slugService;

        public RecipesController(ILogger<HomeController> logger, ApplicationDbContext dbContext, ISlugService slugService)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.slugService = slugService;
        }

        public IActionResult Index(int page = 1, int pageSize = 12)
        {
            if (User.IsInRole("Admin"))
            {
                return View(dbContext.Recipes.OrderByDescending(x => x.Created).ToPagedList(page, pageSize));
            }
            return View(dbContext.Recipes.Where(x => x.Active).OrderByDescending(x => x.Created).ToPagedList(page, pageSize));
        }

        public async Task<IActionResult> Details(string id)
        {
            if(id.IsInteger())
            {
                return await GetRecipe(int.Parse(id));
            }
            return await GetRecipe(slugService.GetIdFromSlug(id));             
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Recipe recipe, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var image = new Image(file);
                    recipe.Image = image.GetBytes();
                    recipe.Thumbnail = image.GetThumbnailBytes();
                }

                recipe.SetCreated(User.FindFirstValue(ClaimTypes.NameIdentifier));
                dbContext.Recipes.Add(recipe);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            return await GetRecipe(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Recipe recipe, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var image = new Image(file);
                    recipe.Image = image.GetBytes();
                    recipe.Thumbnail = image.GetThumbnailBytes();
                }

                dbContext.Entry(recipe).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Details", new { id = recipe.RecipeId });
            }
            return View(recipe);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            return await GetRecipe(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Recipe recipe = await dbContext.Recipes.FindAsync(id);
            dbContext.Recipes.Remove(recipe);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<IActionResult> GetRecipe(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Recipe recipe = await dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
