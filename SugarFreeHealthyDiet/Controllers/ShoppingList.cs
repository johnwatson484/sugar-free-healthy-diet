using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SugarFreeHealthyDiet.Data;
using SugarFreeHealthyDiet.Models;
using System.Collections.Generic;
using System.IO;

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

        public FileResult Download(int[] recipeIds)
        {
            var recipes = dbContext.Recipes.Where(x => recipeIds.Contains(x.RecipeId)).ToList();

            StringWriter sw = new StringWriter();

            foreach (var recipe in recipes)
            {
                sw.WriteLine("# {0}",recipe.Title);
                sw.WriteLine();
                sw.WriteLine(recipe.Ingredients);
                sw.WriteLine();
            }

            return File(new System.Text.UTF8Encoding().GetBytes(sw.ToString()), System.Net.Mime.MediaTypeNames.Application.Octet, "shopping-list.txt");
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
