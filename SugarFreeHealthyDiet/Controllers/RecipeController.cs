using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SugarFreeHealthyDiet.Data;
using X.PagedList;

namespace SugarFreeHealthyDiet.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationDbContext dbContext;

        public RecipeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index(int page = 1, int pageSize = 12)
        {
            if (User.IsInRole("Admin"))
            {
                return View(dbContext.Recipes.OrderByDescending(x => x.Created).ToPagedList(page, pageSize));
            }
            return View(dbContext.Recipes.Where(x => x.Active).OrderByDescending(x => x.Created).ToPagedList(page, pageSize));
        }
    }
}
