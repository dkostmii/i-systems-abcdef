using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab02;

namespace Lab02_Test
{
    [TestClass]
    public class MoneyTest
    {
        [TestMethod]
        public void PassesEqualityCheck()
        {
            decimal amount = (decimal) 0.25;
            Money expected = new Money(amount);
            Money actual = new Money(amount);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NegativeAmountThrows()
        {
            decimal negativeAmount = (decimal) -0.25;
            if (negativeAmount < 0)
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    Money money = new Money(negativeAmount);
                });
            }
            else
                Assert.Fail();
        }
    }
}
