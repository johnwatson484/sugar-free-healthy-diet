using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SugarFreeHealthyDiet.Models;
using SugarFreeHealthyDiet.Data;

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

        public IActionResult Index()
        {
            return View();
		}
	}
}