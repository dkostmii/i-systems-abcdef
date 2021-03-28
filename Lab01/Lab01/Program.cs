using System;
using System.Collections.Generic;
using System.Collections;

using System.Threading.Tasks;

using Lab01.Bags;
using Lab01.Core;
using Lab01.Util;

using System.Linq;

namespace Lab01
{
    class Program
    {   
        HashSet<String> fruits;
        HashSet<String> vegetables;
        HashSet<String> berries;

        List<String> fruitBasket;
        List<String> bagWithVegetables;
        List<String> berryPackage;

        static void Main(string[] args)
        {
            Program instance = new Program();
            instance.Start().Wait();
        }

        Program()
        {
            Init();
        }

        void Init()
        {
            // What we can put in the bags
            fruits = new HashSet<String>(new string[]
            { "Banana", "Apple", "Orange",
              "Lemon", "Grape", "Kiwi", "Lime", "Mango" });

            vegetables = new HashSet<String>(new string[]
            { "Carrot", "Cabbage", "Tomato", "Sweet pepper" });


            berries = new HashSet<String>(new string[]
            { "Raspberry", "Cherry", "Blueberry", "Cranberry" });

            fruitBasket = RandomBagContents(fruits);
            bagWithVegetables = RandomBagContents(vegetables);
            berryPackage = RandomBagContents(berries);
        }

        public async Task Start()
        {
            // Create juicer
            Juicer juicer = Juicer.GetInstance();
            Glass glass = new Glass();

            // Put products into bags
            ProductBag basketWithFruits = new FruitBasket(fruits);
            PutAll(basketWithFruits, fruitBasket);

            ProductBag vegetableBag = new VegetableBag(vegetables);
            PutAll(vegetableBag, bagWithVegetables);

            ProductBag packagedBerries = new BerryPackage(berries);
            PutAll(packagedBerries, berryPackage);


            // Put glass into juicer
            juicer.Glass = glass;

            // Verbose contents of bags
            Console.WriteLine("What we have in the bags???");
            Console.WriteLine(vegetableBag.ToString());
            Console.WriteLine(basketWithFruits.ToString() + "\n");

            Console.WriteLine("Making some juice and mixing!");

            // Take random product, peel and slice it
            // as we only have Juicy products in our bags
            Juicy randomVegetable = vegetableBag.TakeProduct(RandomItem(bagWithVegetables)) as Juicy;
            await juicer.makeSomeJuice(randomVegetable.Peel().Slice());

            Juicy randomFruit = basketWithFruits.TakeProduct(RandomItem(fruitBasket)) as Juicy;
            await juicer.makeSomeJuice(randomFruit.Peel().Slice());


            // Take the glass from juicer
            glass = juicer.Glass;

            // Verbose glass
            Console.WriteLine(glass.ToString() + "\n");

            // Drink the Juice
            Console.WriteLine("Now we are drinking it!");
            Juice juice = glass.Empty();

            // Verbose glass
            Console.WriteLine(glass.ToString());

        }

        

        void PutAll(ProductBag productBag, IEnumerable<String> products)
        {
            foreach (String product in products)
            {
                productBag.PutProduct(product);
            }
        }

        List<String> RandomBagContents(HashSet<String> fromProducts)
        {
            return Randomize.RandomCollection(fromProducts, 1, 4);
        }

        String RandomItem(List<String> bagContents)
        {
            return Randomize.RandomItem<String>(bagContents.ToHashSet());
        }
    }
}
