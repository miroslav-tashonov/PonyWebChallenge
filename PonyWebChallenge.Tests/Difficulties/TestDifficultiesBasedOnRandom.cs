using NUnit.Framework;
using PonyWebChallenge.Difficulties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonyWebChallenge.Tests.Difficulties
{
    [TestFixture()]
    public class TestDifficultiesBasedOnRandom
    {
        [Test()]
        public void TestGetRandomMovement()
        {
            Assert.IsTrue(DifficultiesBasedOnRandom.GetRandomMovement(0.0) >= 0
                && DifficultiesBasedOnRandom.GetRandomMovement(0.0) < 5);
        }

    }
}
