using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab02.ToyMachine;

namespace Lab02
{
    class SoftToysMachine : Machine
    {
        public SoftToysMachine(MoneyBox moneyBox,
                        ToyBox toyBox,
                        MoneyReceiver moneyReceiver,
                        MoneyExtractor moneyExtractor,
                        MoneyDispenser moneyDispenser,
                        ToyTray toyTray,
                        HashSet<decimal> acceptsMoney) : base(moneyBox, toyBox, moneyReceiver, moneyExtractor, moneyDispenser, toyTray, acceptsMoney)
        {

        }

        override protected void InstallMoneyTransmitter(MoneyReceiver moneyReceiver, MoneyExtractor moneyExtractor)
        {
            this.moneyTransmitter = new MoneyTransmitter(moneyReceiver, moneyExtractor, this.acceptsMoney, this.moneyDispenser);
        }

        override protected void InstallBoxes(MoneyBox moneyBox, ToyBox toyBox)
        {
            this.moneyBox = moneyBox;
            this.toyBox = toyBox;
        }

        override protected void InstallToyTray(ToyTray toyTray)
        {
            this.toyTray = toyTray;
        }

        override protected void InstallMoneyDispenser(MoneyDispenser moneyDispenser)
        {
            this.moneyDispenser = moneyDispenser;
        }

        override protected void InstallToyExtractor()
        {
            this.toyExtractor = new ToyExtractor(this.toyBox, this.moneyTransmitter, this.toyTray);
        }
    }
}
