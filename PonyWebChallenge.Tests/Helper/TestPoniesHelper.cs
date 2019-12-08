using NUnit.Framework;
using PonyWebChallenge.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PonyWebChallenge.Tests.Helper
{
    [TestFixture()]
    public class TestPoniesHelper
    {
        [Test()]
        public void GetPoniesTest()
        {
            IEnumerable<SelectListItem> ponies = PoniesHelper.GetPonies();
            Assert.IsTrue(ponies.Count() == PoniesHelper.PonyNames.Count());
        }
    }
}
