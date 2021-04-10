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
    public class JuicerTests
    {
        [TestMethod]
        public void GetInstanceReturnsSameInstance()
        {
            Juicer firstJuicer = Juicer.GetInstance();
            Juicer secondJuicer = Juicer.GetInstance();

            Assert.ReferenceEquals(firstJuicer, secondJuicer);
        }

        [TestMethod]
        public void GettingGlassFromEmptyJuicerThrows()
        {
            Juicer juicer = Juicer.GetInstance();

            Assert.ThrowsException<Exception>(() => { Glass glass = juicer.Glass; });

        }

        [TestMethod]
        public void PuttingOneMoreGlassThrows()
        {
            Juicer juicer = Juicer.GetInstance();

            Assert.ThrowsException<Exception>(() => 
            {
                juicer.Glass = new Glass();
                juicer.Glass = new Glass(); 
            });

            Glass glass = juicer.Glass;
        }

        [TestMethod]
        public void MakingJuiceWithoutGlassThrows()
        {
            Juicer juicer = Juicer.GetInstance();

            Product apple = new Fruit("Apple").Peel().Slice();
            Product cherry = new Berry("Cherry").Peel().Slice();
            Product cabbage = new Vegetable("Cabbage").Peel().Slice();

            List<Juicy> juicyProducts = new List<Juicy>();
            juicyProducts.Add(apple as Juicy);
            juicyProducts.Add(cherry as Juicy);
            juicyProducts.Add(cabbage as Juicy);

            Assert.ThrowsExceptionAsync<Exception>(() => juicer.makeSomeJuice(apple as Juicy));
            Assert.ThrowsExceptionAsync<Exception>(() => juicer.makeGlassOfJuice(juicyProducts));

        }

        [TestMethod]
        public void MakingJuiceFromSingleProductWorksProperly()
        {
            Juicer juicer = Juicer.GetInstance();

            Glass glass = new Glass();
            juicer.Glass = glass;

            Juicy apple = new Fruit("Apple").Peel().Slice() as Juicy;

            juicer.makeSomeJuice(apple).Wait();

            List<String> juicyProductNames = new List<String>();
            juicyProductNames.Add(apple.Name);

            CollectionAssert.AreEqual(juicer.Glass.Empty().GetIngredients(), juicyProductNames);



            juicer.Glass = glass;

            apple = new Fruit("Apple").Peel().Slice() as Juicy;
            List<Juicy> juicyProducts = new List<Juicy>();
            juicyProducts.Add(apple);

            juicer.makeGlassOfJuice(juicyProducts).Wait();

            CollectionAssert.AreEqual(juicer.Glass.Empty().GetIngredients(), juicyProductNames);
        }

        [TestMethod]
        public void MakingJuiceFromMultipleProductsWorksProperly()
        {
            Juicer juicer = Juicer.GetInstance();

            Glass glass = new Glass();
            juicer.Glass = glass;

            Juicy apple = new Fruit("Apple").Peel().Slice() as Juicy;
            Juicy tomato = new Vegetable("Tomato").Peel().Slice() as Juicy;
            Juicy cherry = new Berry("Cherry").Peel().Slice() as Juicy;

            juicer.makeSomeJuice(apple).Wait();
            juicer.makeSomeJuice(tomato).Wait();
            juicer.makeSomeJuice(cherry).Wait();

            List<String> juicyProductNames = new List<String>();
            juicyProductNames.Add(apple.Name);
            juicyProductNames.Add(tomato.Name);
            juicyProductNames.Add(cherry.Name);

            CollectionAssert.AreEqual(juicer.Glass.Empty().GetIngredients(), juicyProductNames);



            juicer.Glass = glass;

            apple = new Fruit("Apple").Peel().Slice() as Juicy;
            tomato = new Vegetable("Tomato").Peel().Slice() as Juicy;
            cherry = new Berry("Cherry").Peel().Slice() as Juicy;

            List<Juicy> juicyProducts = new List<Juicy>();
            juicyProducts.Add(apple);
            juicyProducts.Add(tomato);
            juicyProducts.Add(cherry);

            juicer.makeGlassOfJuice(juicyProducts).Wait();

            CollectionAssert.AreEqual(juicer.Glass.Empty().GetIngredients(), juicyProductNames);
        }
    }
}
