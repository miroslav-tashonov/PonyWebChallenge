using NUnit.Framework;
using PonyWebChallenge.Controllers;
using PonyWebChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PonyWebChallenge.Tests.Controllers
{
    [TestFixture()]
    public class TestMazeViewController
    {
        private Guid MazeId = Guid.NewGuid();

        [Test()]
        public void IndexTest()
        {
            var controller = new MazeViewController();
            System.Web.Mvc.ViewResult result = (System.Web.Mvc.ViewResult)controller.Index();
            Assert.IsTrue(result.ViewData.Count == 2);
        }

        [Test()]
        public void MazeAction()
        {
            var controller = new MazeViewController();
            System.Web.Mvc.ViewResult result = (System.Web.Mvc.ViewResult)controller.MazeAction(MazeId);
            Assert.IsTrue(((MazeActionModel)result.Model).MazeId != Guid.Empty);
        }

        [Test()]
        public void SuccessfullEndgame()
        {
            var controller = new MazeViewController();
            System.Web.Mvc.ViewResult result = (System.Web.Mvc.ViewResult)controller.SuccessfullEndgame(MazeId);
            Assert.IsTrue(((MazeActionModel)result.Model).MazeId != Guid.Empty);
        }

        [Test()]
        public void FailedEndgame()
        {
            var controller = new MazeViewController();
            System.Web.Mvc.ViewResult result = (System.Web.Mvc.ViewResult)controller.FailedEndgame(MazeId);
            Assert.IsTrue(((MazeActionModel)result.Model).MazeId != Guid.Empty);
        }
    }
}
