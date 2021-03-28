using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab01.Core;
using Lab01.Products;

namespace Lab01_Test.Core
{
    [TestClass]
    public class JuicyTests
    {
        [TestMethod]
        public void ProductHasCorrectName()
        {
            String expected = "Cherry";
            Product berry = new Berry(expected);

            Assert.AreEqual(expected, berry.Name);

            expected = "Tomato";
            Product vegetable = new Vegetable(expected);

            Assert.AreEqual(expected, vegetable.Name);

            expected = "Apple";
            Product fruit = new Fruit(expected);

            Assert.AreEqual(expected, fruit.Name);
        }

        [TestMethod]
        public void CreatingWithEmptyNameThrows()
        {
            Assert.ThrowsException<Exception>(() => new Berry(""));
            Assert.ThrowsException<Exception>(() => new Fruit(""));
            Assert.ThrowsException<Exception>(() => new Vegetable(""));
        }

        [TestMethod]
        public void ExtractingOneMoreTimeThrows()
        {
            Juicy cherry = new Berry("Cherry") as Juicy;
            Juice extracted = cherry.Juice;

            Assert.ThrowsException<Exception>(() => { Juice mustThrow = cherry.Juice; });

            Juicy apple = new Fruit("Apple") as Juicy;
            extracted = apple.Juice;

            Assert.ThrowsException<Exception>(() => { Juice mustThrow = apple.Juice; });

            Juicy carrot = new Vegetable("Carrot") as Juicy;
            extracted = carrot.Juice;

            Assert.ThrowsException<Exception>(() => { Juice mustThrow = carrot.Juice; });
        }

        [TestMethod]
        public void SliceAndPeelWorkingInChain()
        {
            Juicy expected = new Fruit("Apple") as Juicy;
            Juicy actual = expected.Slice().Peel();

            Assert.IsTrue(expected.IsPeeled());
            Assert.IsTrue(expected.IsSliced());

            Assert.ReferenceEquals(expected, actual);

            Assert.IsTrue(actual.IsPeeled());
            Assert.IsTrue(actual.IsSliced());



            expected = new Vegetable("Tomato") as Juicy;
            actual = expected.Slice().Peel();

            Assert.IsTrue(expected.IsPeeled());
            Assert.IsTrue(expected.IsSliced());

            Assert.ReferenceEquals(expected, actual);

            Assert.IsTrue(actual.IsPeeled());
            Assert.IsTrue(actual.IsSliced());



            expected = new Berry("Cherry") as Juicy;
            actual = expected.Slice().Peel();

            Assert.IsTrue(expected.IsPeeled());
            Assert.IsTrue(expected.IsSliced());

            Assert.ReferenceEquals(expected, actual);

            Assert.IsTrue(actual.IsPeeled());
            Assert.IsTrue(actual.IsSliced());
        }
    }
}
