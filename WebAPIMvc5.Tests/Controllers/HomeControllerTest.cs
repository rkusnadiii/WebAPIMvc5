using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WebAPIMvc5;
using WebAPIMvc5.Controllers;

namespace WebAPIMvc5.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
