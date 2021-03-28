using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab01.Core;
using Lab01.Bags;
using Lab01.Products;

namespace Lab01_Test.Core
{
    [TestClass]
    public class ProductBagTests
    {
        HashSet<String> fruits = new HashSet<String>(new string[]
            { "Banana", "Apple", "Orange",
              "Lemon", "Grape", "Kiwi", "Lime", "Mango" });

        HashSet<String> vegetables = new HashSet<String>(new string[]
            { "Carrot", "Cabbage", "Tomato", "Sweet pepper" });


        HashSet<String> berries = new HashSet<String>(new string[]
            { "Raspberry", "Cherry", "Blueberry", "Cranberry" });

        [TestMethod]
        public void TakingWithEmptyNameThrows()
        {
            ProductBag berryPackage = new BerryPackage(berries);
            ProductBag vegetableBag = new VegetableBag(vegetables);
            ProductBag fruitBasket = new FruitBasket(fruits);

            Assert.ThrowsException<Exception>(() => berryPackage.TakeProduct(""));
            Assert.ThrowsException<Exception>(() => vegetableBag.TakeProduct(""));
            Assert.ThrowsException<Exception>(() => fruitBasket.TakeProduct(""));
        }

        [TestMethod]
        public void TakingFromEmptyBagThrows()
        {
            ProductBag berryPackage = new BerryPackage(berries);
            ProductBag vegetableBag = new VegetableBag(vegetables);
            ProductBag fruitBasket = new FruitBasket(fruits);

            Assert.ThrowsException<Exception>(() => berryPackage.TakeProduct("Blueberry"));
            Assert.ThrowsException<Exception>(() => vegetableBag.TakeProduct("Tomato"));
            Assert.ThrowsException<Exception>(() => fruitBasket.TakeProduct("Orange"));
        }

        [TestMethod]
        public void PuttingProductWorksProperly()
        {
            ProductBag berryPackage = new BerryPackage(berries);
            ProductBag vegetableBag = new VegetableBag(vegetables);
            ProductBag fruitBasket = new FruitBasket(fruits);

            String berryProductName = "Blueberry";
            String vegetableProductName = "Tomato";
            String fruitProductName = "Orange";

            berryPackage.PutProduct(berryProductName);
            vegetableBag.PutProduct(vegetableProductName);
            fruitBasket.PutProduct(fruitProductName);

            Assert.AreEqual(berryProductName, berryPackage.TakeProduct(berryProductName).Name);
            Assert.ThrowsException<Exception>(() => 
            {
                berryPackage.TakeProduct(berryProductName);
            });

            Assert.AreEqual(vegetableProductName, vegetableBag.TakeProduct(vegetableProductName).Name);
            Assert.ThrowsException<Exception>(() =>
            {
                vegetableBag.TakeProduct(vegetableProductName);
            });


            Assert.AreEqual(fruitProductName, fruitBasket.TakeProduct(fruitProductName).Name);
            Assert.ThrowsException<Exception>(() =>
            {
                fruitBasket.TakeProduct(fruitProductName);
            });
        }

        [TestMethod]
        public void TakingUnexistingProductThrows()
        {
            ProductBag berryPackage = new BerryPackage(berries);
            ProductBag vegetableBag = new VegetableBag(vegetables);
            ProductBag fruitBasket = new FruitBasket(fruits);

            berryPackage.PutProduct("Blueberry");
            vegetableBag.PutProduct("Tomato");
            fruitBasket.PutProduct("Orange");

            Assert.ThrowsException<Exception>(() => berryPackage.TakeProduct("Cherry"));
            Assert.ThrowsException<Exception>(() => vegetableBag.TakeProduct("Sweet pepper"));
            Assert.ThrowsException<Exception>(() => fruitBasket.TakeProduct("Lime"));
        }

        [TestMethod]
        public void PuttingUnexistingProductThrows()
        {
            ProductBag berryPackage = new BerryPackage(berries);
            ProductBag vegetableBag = new VegetableBag(vegetables);
            ProductBag fruitBasket = new FruitBasket(fruits);

            Assert.ThrowsException<Exception>(() => berryPackage.PutProduct("Orange"));
            Assert.ThrowsException<Exception>(() => vegetableBag.PutProduct("Blueberry"));
            Assert.ThrowsException<Exception>(() => fruitBasket.PutProduct("Tomato"));
        }


        [TestMethod]
        public void ProductBagsReturnProperTypes()
        {
            ProductBag berryPackage = new BerryPackage(berries);
            ProductBag vegetableBag = new VegetableBag(vegetables);
            ProductBag fruitBasket = new FruitBasket(fruits);

            String berryProductName = "Blueberry";
            String vegetableProductName = "Tomato";
            String fruitProductName = "Orange";

            berryPackage.PutProduct(berryProductName);
            vegetableBag.PutProduct(vegetableProductName);
            fruitBasket.PutProduct(fruitProductName);

            Assert.IsInstanceOfType(berryPackage.TakeProduct(berryProductName), typeof(Berry));
            Assert.IsInstanceOfType(vegetableBag.TakeProduct(vegetableProductName), typeof(Vegetable));
            Assert.IsInstanceOfType(fruitBasket.TakeProduct(fruitProductName), typeof(Fruit));
        }

    }
}
