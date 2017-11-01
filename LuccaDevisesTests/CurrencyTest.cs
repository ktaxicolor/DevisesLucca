
using LuccaDevises;
using NUnit.Framework;
using System;

namespace LuccaDevisesTests
{
    [TestFixture]
    public class CurrencyTest
    {
        private const string VALID_TRIGRAM = "VAL";
        private const string VALID_TRIGRAM_1 = "VA1";
        private const string INVALID_TRIGRAM = "INVA";

        [Test]
        public void should_set_trigram_if_valid()
        {
            var curr = new Currency(VALID_TRIGRAM);
            Assert.AreEqual(VALID_TRIGRAM, curr.Trigram);
        }

        [Test]
        public void should_get_trigram_as_upperCase()
        {
            var curr = new Currency(VALID_TRIGRAM.ToLowerInvariant());
            Assert.AreEqual(VALID_TRIGRAM, curr.Trigram);
        }

        [Test]
        public void should_throw_FormatException_if_trigram_is_too_long()
        {
            Assert.Throws<FormatException>(() => new Currency(INVALID_TRIGRAM));
        }

        [Test]
        public void should_throw_FormatException_if_trigram_is_null()
        {
            Assert.Throws<FormatException>(() => new Currency(null));
        }

        [Test]
        public void should_return_true_if_currencies_have_same_trigram()
        {
            var curr1 = new Currency(VALID_TRIGRAM);
            var curr2 = new Currency(VALID_TRIGRAM);

            Assert.IsTrue(curr1.Equals(curr2));
        }

        [Test]
        public void should_return_false_if_currencies_have_different_trigrams()
        {
            var curr1 = new Currency(VALID_TRIGRAM);
            var curr2 = new Currency(VALID_TRIGRAM_1);

            Assert.IsFalse(curr1.Equals(curr2));
        }
    }
}
