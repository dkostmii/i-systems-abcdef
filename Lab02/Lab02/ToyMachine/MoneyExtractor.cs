using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.ToyMachine
{
    class MoneyExtractor
    {
        private MoneyBox moneyBox;
        private List<Money> buffer;

        public MoneyExtractor(MoneyBox moneyBox)
        {
            this.moneyBox = moneyBox;
        }

        public void ExtractSome(decimal amount)
        {
            if (moneyBox.HowMuch(amount) > 0)
            {
                buffer.Add(moneyBox.Get(amount));
            }
            else
            {
                throw new Exception($"Lack of amount: {amount} in money box");
            }
        }

        public List<Money> PushAll()
        {
            if (buffer.Count > 0)
                return buffer;

            throw new Exception("Cannot push from empty buffer");
        }

        public void DiscardAll()
        {
            if (buffer.Count > 0)
                moneyBox.Put(buffer);

            throw new Exception("Cannot push the empty buffer");
        }
    }
}
