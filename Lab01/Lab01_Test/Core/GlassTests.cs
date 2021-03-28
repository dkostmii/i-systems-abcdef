using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab01.Core;

namespace Lab01_Test.Core
{
    [TestClass]
    public class GlassTests
    {
        [TestMethod]
        public void EmptyTheEmptyGlassThrows()
        {
            Glass glass = new Glass();

            Assert.ThrowsException<Exception>(() => glass.Empty());
        }

        [TestMethod]
        public void PourAndEmptySameJuice()
        {
            Juice expected = new Juice("Mango");

            Glass glass = new Glass();
            glass.Pour(expected);

            Juice actual = glass.Empty();

            Assert.ReferenceEquals(expected, actual);
        }

        [TestMethod]
        public void PouringMultipleJuicesMixes()
        {
            Juice mangoJuice = new Juice("Mango");
            Juice bananaJuice = new Juice("Banana");
            List<String> expected = mangoJuice.Mix(bananaJuice).GetIngredients();

            Glass glass = new Glass();

            glass.Pour(mangoJuice);
            glass.Pour(bananaJuice);

            List<String> actual = glass.Empty().GetIngredients();

            CollectionAssert.AreEqual(expected, actual);

        }
    }
}
