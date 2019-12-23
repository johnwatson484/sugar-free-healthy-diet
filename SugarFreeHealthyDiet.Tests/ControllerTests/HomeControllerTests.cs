using NUnit.Framework;
using SugarFreeHealthyDiet.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace SugarFreeHealthyDiet.Tests
{
    public class Tests
    {
        Mock<ILogger<HomeController>> logger;
        HomeController controller;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<HomeController>>();
            controller = new HomeController(logger.Object);
        }

        [Test]
        public void Test_Home_Controller_Redirects_To_Recipes()
        {
            var result = controller.Index() as RedirectToActionResult;
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Recipe", result.ControllerName);
        }
    }
}
