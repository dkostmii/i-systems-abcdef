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
    public class ToyTest
    {
        [TestMethod]
        public void EmptyNameThrows()
        {
            String emptyName = "";

            if (String.IsNullOrEmpty(emptyName))
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    Toy toy = new Toy { Name = emptyName, Price = (decimal)0.25 };
                });
            }
            else
                Assert.Fail();
        }

        [TestMethod]
        public void NegativePriceThrows()
        {
            decimal negativePrice = (decimal) -2.5;

            if (negativePrice < 0)
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    Toy toy = new Toy { Name = "Mickey Mouse", Price = negativePrice };
                });
            }
            else
                Assert.Fail();
        }
    }
}
