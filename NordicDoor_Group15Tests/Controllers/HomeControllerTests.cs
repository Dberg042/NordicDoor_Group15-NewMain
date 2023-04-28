using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordicDoor_Group15.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordicDoor_Group15.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private readonly ILogger<HomeController> _logger;

        [TestMethod()]
        public void IndexTest()
        {
            var controller = new HomeController(_logger);
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("index", result.ViewName);
        }

        [TestMethod()]
        public void IndexText2()
        {
            var controller = new HomeController(_logger);
            var result = controller.Index();
            Assert.IsNotNull(result);
        }

        
    }
}