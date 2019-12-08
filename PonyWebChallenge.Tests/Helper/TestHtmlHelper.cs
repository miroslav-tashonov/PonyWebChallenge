using NUnit.Framework;
using PonyWebChallenge.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PonyWebChallenge.Models.CellStateEnumModel;

namespace PonyWebChallenge.Tests.Helper
{
    [TestFixture()]
    public class TestHtmlHelper
    {
        [Test()]
        public void GenerateTopBorderTest()
        {
            Assert.IsTrue(HtmlHelper.GenerateTopBorder() == "+---");
        }

        [Test()]
        public void GenerateBorderTest()
        {
            Assert.IsTrue(HtmlHelper.GenerateBorder() == "|");
        }

        [Test()]
        public void GenerateInterectionTest()
        {
            Assert.IsTrue(HtmlHelper.GenerateInterection() == "+");
        }

        [Test()]
        public void GenerateBreakTest()
        {
            Assert.IsTrue(HtmlHelper.GenerateBreak() == "<br/>");
        }

        [Test()]
        public void GenerateBlankTopBorderTest()
        {
            Assert.IsTrue(HtmlHelper.GenerateBlankTopBorder() == "+&nbsp;&nbsp;&nbsp;");
        }

        [Test()]
        public void GenerateWestBorderTest()
        {
            CellState cellStateDomokun = CellState.Domokun;
            CellState cellStatePony = CellState.Pony;
            CellState cellStateEndpoint = CellState.Endpoint;
            CellState cellState = CellState.Visited;

            Assert.IsTrue(HtmlHelper.GenerateWestBorder(cellStateDomokun) == "|&nbsp;D&nbsp;");
            Assert.IsTrue(HtmlHelper.GenerateWestBorder(cellStatePony) == "|&nbsp;P&nbsp;");
            Assert.IsTrue(HtmlHelper.GenerateWestBorder(cellStateEndpoint) == "|&nbsp;E&nbsp;");
            Assert.IsTrue(HtmlHelper.GenerateWestBorder(cellState) == "|&nbsp;&nbsp;&nbsp;");
        }

        [Test()]
        public void GenerateBlankWestBorderTest()
        {
            CellState cellStateDomokun = CellState.Domokun;
            CellState cellStatePony = CellState.Pony;
            CellState cellStateEndpoint = CellState.Endpoint;
            CellState cellState = CellState.Visited;

            Assert.IsTrue(HtmlHelper.GenerateBlankWestBorder(cellStateDomokun) == "&nbsp;&nbsp;D&nbsp;");
            Assert.IsTrue(HtmlHelper.GenerateBlankWestBorder(cellStatePony) == "&nbsp;&nbsp;P&nbsp;");
            Assert.IsTrue(HtmlHelper.GenerateBlankWestBorder(cellStateEndpoint) == "&nbsp;&nbsp;E&nbsp;");
            Assert.IsTrue(HtmlHelper.GenerateBlankWestBorder(cellState) == "&nbsp;&nbsp;&nbsp;&nbsp;");
        }

    }
}
