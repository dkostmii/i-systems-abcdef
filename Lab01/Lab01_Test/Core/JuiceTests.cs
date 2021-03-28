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
    public class JuiceTests
    {
        [TestMethod]
        public void EmptyExtractedFromThrows()
        {
            Assert.ThrowsException<Exception>(() => new Juice(""));
        }

        [TestMethod]
        public void JuiceHasProperIngredients()
        {
            String name = "Carrot";
            List<String> expected = new List<String>();
            expected.Add(name);

            Juice carrotJuice = new Juice(name);

            List<String> actual = carrotJuice.GetIngredients();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MixingJuiceWorksProperly()
        {
            String vegetableName = "Carrot";
            String fruitName = "Apple";

            Juice carrotJuice = new Juice(vegetableName);
            Juice appleJuice = new Juice(fruitName);

            List<String> expected = new List<String>();
            expected.Add(vegetableName);
            expected.Add(fruitName);

            List<String> actual = carrotJuice.Mix(appleJuice).GetIngredients();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
