using ApprovalTests;
using ApprovalTests.Reporters;
using LuccaDevises.Utils;
using NUnit.Framework;
using System;


namespace LuccaDevisesTests.Utils
{
    [TestFixture]
    public class AccountingUtilsTest
    {
        #region Rounding rate
        [Test]
        public void Should_round_double_at_4_decimals()
        {
            var expectedRounded = Math.Round(TestConst.RATE_SIX_DECIMALS, 4);

            var rounded = AccountingUtils.RoundRate(TestConst.RATE_SIX_DECIMALS);

            Assert.AreEqual(expectedRounded, rounded);
        }
        #endregion

        #region Computing amount
        [UseReporter(typeof(DiffReporter))]
        [Test]
        public void Should_multiply_rates_to_compute_correct_final_amount()
        {
            var bestNode = TestHelper.GetABestNode();

            var calculatedResult = AccountingUtils.ComputeResultAmount(bestNode, TestConst.STARTING_AMOUNT);

            Approvals.Verify(calculatedResult);
        }
        #endregion
    }
}
