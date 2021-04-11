using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.ToyMachine
{
    class MoneyReceiver
    {
        private MoneyBox moneyBox;
        private List<Money> buffer;

        public MoneyReceiver(MoneyBox moneyBox)
        {
            this.moneyBox = moneyBox;
            buffer = new List<Money>();
        }

        public void PutSome(Money money)
        {
            buffer.Add(money);
        }

        public List<Money> ReturnAll()
        {
            List<Money> returning = buffer;
            buffer = new List<Money>();
            return returning;
        }

        public Dictionary<decimal, int> PushAll(Summary countMoney)
        {
            if (buffer.Count > 0)
            {
                moneyBox.Put(buffer);
                return countMoney(buffer);
            }

            throw new Exception("Cannot push the empty buffer.");
        }
    }
}
