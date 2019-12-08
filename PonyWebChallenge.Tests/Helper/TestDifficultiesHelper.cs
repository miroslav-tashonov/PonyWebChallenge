using NUnit.Framework;
using PonyWebChallenge.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static PonyWebChallenge.Models.DifficultiesEnum;

namespace PonyWebChallenge.Tests.Helper
{
    [TestFixture()]
    public class TestDifficultiesHelper
    {
        [Test()]
        public void GetDifficultiesTest()
        {
            IEnumerable<SelectListItem> difficulties = DifficultiesHelper.GetDifficulties();

            Assert.IsTrue(Enum.GetNames(typeof(Difficulty)).Length == difficulties.Count());
        }
    }
}
