using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SugarFreeHealthyDiet.Data;
using SugarFreeHealthyDiet.Models;
using SugarFreeHealthyDiet.Common;
using SugarFreeHealthyDiet.Services;
using SugarFreeHealthyDiet.Extensions;
using X.PagedList;

namespace SugarFreeHealthyDiet.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationDbContext dbContext;

        public ShoppingListController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index(int[] recipeIds)
        {
            return View(dbContext.Recipes.Where(x => recipeIds.Contains(x.RecipeId)).ToList());
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
