using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.ToyMachine
{
    class ToyExtractor
    {
        private ToyBox toyBox;
        private MoneyTransmitter moneyTransmitter;
        private ToyTray toyTray;

        public ToyExtractor(ToyBox toyBox, 
                     MoneyTransmitter moneyTransmitter,
                     ToyTray toyTray)
        {
            this.toyBox = toyBox;
            this.moneyTransmitter = moneyTransmitter;
            this.toyTray = toyTray;
        }

        public void ExtractToy()
        {
            Toy toy = toyBox.GetToy();
            if (moneyTransmitter.Credit - toy.Price >= 0)
            {
                moneyTransmitter.CreditOperation(-toy.Price);
                toyTray.PutToy(toy);
            }
            else
            {
                toyBox.PutToys(new List<Toy> { toy });
            }
        }
    }
}
