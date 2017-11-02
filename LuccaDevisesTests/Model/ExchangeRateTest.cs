using LuccaDevises;
using LuccaDevises.Utils;
using NUnit.Framework;

namespace LuccaDevisesTests.Model
{
    [TestFixture]
    public class ExchangeRateTest
    {
        private ExchangeRate exchRate;

        [SetUp]
        public void SetUp()
        {
            exchRate = TestHelper.GetExchangeRate();
        }

        #region Setting rate
        [Test]
        public void Should_round_rate_when_setting_it()
        {
            exchRate.Rate = TestConst.RATE_SIX_DECIMALS;
            
            Assert.AreEqual(AccountingUtils.RoundRate(TestConst.RATE_SIX_DECIMALS), exchRate.Rate);
        }
        #endregion

        #region Inversing rate
        [Test]
        public void Should_inverse_exchange_rate()
        {
            var expectedRate = AccountingUtils.RoundRate(1 / exchRate.Rate);
            exchRate.InverseRate();
            Assert.AreEqual(expectedRate, AccountingUtils.RoundRate(exchRate.Rate));

        }

        [Test]
        public void Should_return_rate_when_rate_is_inversed()
        {
            Assert.AreEqual(exchRate, exchRate.InverseRate());
        }

        [Test]
        public void Should_swap_initial_and_final_currencies_when_rate_is_inversed()
        {
            var expectedInitialCurrency = exchRate.FinalCurrency;
            var expectedFinalCurrency = exchRate.InitialCurrency;

            exchRate.InverseRate();

            Assert.AreEqual(expectedInitialCurrency, exchRate.InitialCurrency);
            Assert.AreEqual(expectedFinalCurrency, exchRate.FinalCurrency);
        }
        #endregion
    }
}

