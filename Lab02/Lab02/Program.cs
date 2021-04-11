using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab02.ToyMachine;

namespace Lab02
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Money> machineMoney = SpawnMoney(
                new Dictionary<decimal, int>()
                {
                    [(decimal)0.1] = 200,
                    [(decimal)0.2] = 175,
                    [(decimal)0.5] = 100,
                    [(decimal)1.0] = 50,
                    [(decimal)2.0] = 25,
                    [(decimal)5.0] = 10,
                }
            );

            List<Money> ourMoney = SpawnMoney(
                new Dictionary<decimal, int>()
                {
                    [(decimal)0.1] = 10,
                    [(decimal)0.2] = 5,
                    [(decimal)2.0] = 3,
                    [(decimal)5.0] = 4,
                }
            );

            HashSet<decimal> acceptsMoney = new HashSet<decimal>()
            {
                (decimal) 0.1,
                (decimal) 0.2,
                (decimal) 0.5,
                (decimal) 1.0,
                (decimal) 2.0,
                (decimal) 5.0
            };

            List<Toy> softToys = new List<Toy>()
            {
                new SoftToy() { Name = "TeddyBear", Price = (decimal) 0.6 },
                new SoftToy() { Name = "Doggo", Price = (decimal) 1.5 },
                new SoftToy() { Name = "Unicorn", Price = (decimal) 4.5 },
                new SoftToy() { Name = "MikkyMouse", Price = (decimal) 3.0 },
                new SoftToy() { Name = "Tutle", Price = (decimal) 1.75 },
            };

            MoneyBox moneyBox = new MoneyBox();
            ToyBox toyBox = new ToyBox();
            MoneyReceiver moneyReceiver = new MoneyReceiver(moneyBox);
            MoneyExtractor moneyExtractor = new MoneyExtractor(moneyBox);
            ToyTray toyTray = new ToyTray();
            MoneyDispenser moneyDispenser = new MoneyDispenser();

            moneyBox.Put(machineMoney);
            toyBox.PutToys(softToys);


            SoftToysMachine machine = new SoftToysMachine(moneyBox, toyBox, moneyReceiver, moneyExtractor, moneyDispenser, toyTray, acceptsMoney);

            Console.WriteLine("We want some toys! :)\n\n");

            List<Toy> ourToys = machine.WeWantSomeToys(ourMoney, 4);

            foreach (var toy in ourToys)
            {
                Console.WriteLine(toy.ToString());
            }
        }

        public static List<Money> SpawnMoney(Dictionary<decimal, int> moneySummary)
        {
            List<Money> result = new List<Money>();

            foreach (var amount in moneySummary)
            {
                for (int i = 0; i < amount.Value; i++)
                {
                    result.Add(new Money(amount.Key));
                }
            }

            return result;
        }
    }
}
