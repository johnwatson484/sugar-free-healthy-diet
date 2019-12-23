using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SugarFreeHealthyDiet.Models;
using SugarFreeHealthyDiet.Data;
using X.PagedList.Mvc.Core;
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
            return View(dbContext.Recipes.Where(x => x.Active).OrderByDescending(x => x.Created).ToPagedList(page, pageSize));
        }
    }
}
