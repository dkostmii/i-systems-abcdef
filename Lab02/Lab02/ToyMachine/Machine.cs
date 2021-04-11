using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace Lab02.ToyMachine
{
    abstract class Machine
    {
        protected MoneyBox moneyBox;
        protected MoneyDispenser moneyDispenser;
        protected MoneyTransmitter moneyTransmitter;

        protected ToyBox toyBox;
        protected ToyExtractor toyExtractor;
        protected ToyTray toyTray;

        protected HashSet<decimal> acceptsMoney;


        protected Machine(MoneyBox moneyBox,
                ToyBox toyBox,
                MoneyReceiver moneyReceiver,
                MoneyExtractor moneyExtractor,
                MoneyDispenser moneyDispenser,
                ToyTray toyTray,
                HashSet<decimal> acceptsMoney)
        {
            this.acceptsMoney = acceptsMoney;
            assembleMachine(moneyBox, toyBox, moneyReceiver, moneyExtractor, moneyDispenser, toyTray);
        }

        // template for assemble
        public void assembleMachine(MoneyBox moneyBox, 
                                    ToyBox toyBox,
                                    MoneyReceiver moneyReceiver,
                                    MoneyExtractor moneyExtractor,
                                    MoneyDispenser moneyDispenser,
                                    ToyTray toyTray)
        {
            // TODO: Install necessary components
            InstallBoxes(moneyBox, toyBox);
            InstallMoneyDispenser(moneyDispenser);
            InstallMoneyTransmitter(moneyReceiver, moneyExtractor);
            InstallToyTray(toyTray);
            InstallToyExtractor();

            IntegrityCheck();
        }

        // assemble logic

        protected abstract void InstallBoxes(MoneyBox moneyBox, ToyBox toyBox);
        protected abstract void InstallMoneyTransmitter(MoneyReceiver moneyReceiver, MoneyExtractor moneyExtractor);
        protected abstract void InstallToyExtractor();
        protected abstract void InstallMoneyDispenser(MoneyDispenser moneyDispenser);
        protected abstract void InstallToyTray(ToyTray toyTray);


        // template for business logic

        public List<Toy> WeWantSomeToys(List<Money> money, int count)
        {
            TopUpBalance(money);
            if (ShowBalance() > 0)
            {
                if (count == 3)
                {
                    return GetThreeToys();
                }
                else
                {
                    List<Toy> toys = new List<Toy>();
                    for (int i = 0; i < count; i++)
                    {
                        toys.Add(GetToy());
                    }
                    return toys;
                }
            }
            else
            {
                throw new Exception("Where is our money???");
            }
        }

        // business logic

        public decimal ShowBalance()
        {
            return moneyTransmitter.Credit;
        }

        public void PutSomeMoney(List<Money> money)
        {
            foreach (var someMoney in money)
            {
                Money? returnedMoney = moneyTransmitter.ReceiveMoney(someMoney);

                if (returnedMoney != null)
                    throw new Exception($"Invalid amount of money: {returnedMoney.Amount}");
            }
        }
        public void TopUpBalance(List<Money> money)
        {
            PutSomeMoney(money);

            moneyTransmitter.Replenish();
        }

        public Toy GetToy()
        {
            toyExtractor.ExtractToy();
            if (toyTray.Empty)
                throw new Exception("Seems you don't have enough credits :(");

            Toy toy = toyTray.GetToys().First();

            return toy;
        }

        public List<Toy> GetThreeToys()
        {
            return GetToys(3);
        }

        public List<Toy> GetToys(int count)
        {
            List<Toy> toys = new List<Toy>();
            for (int i = 0; i < count; i++)
            {
                toys.Add(GetToy());
            }

            return toys;
        }



        public void IntegrityCheck()
        {
            if (moneyBox == null || toyBox == null || moneyTransmitter == null || toyExtractor == null
                || moneyDispenser == null || toyTray == null)
            {
                throw new Exception("Integrity check failed");
            }
        }

    }
}
