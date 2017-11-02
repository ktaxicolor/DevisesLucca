using LuccaDevises.Utils;
using NUnit.Framework;
using System;


namespace LuccaDevisesTests.Utils
{
    [TestFixture]
    public class AccountingUtilsTest
    {
        private double doubleSixDecimals = TestConst.RATE_SIX_DECIMALS;

        [Test]
        public void Should_round_double_at_4_decimals()
        {
            var expectedRounded = Math.Round(doubleSixDecimals, 4);

            var rounded = AccountingUtils.RoundRate(doubleSixDecimals);

            Assert.AreEqual(expectedRounded, rounded);
        }
    }
}
