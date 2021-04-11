using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.ToyMachine
{
    class MoneyBox
    {
        private List<Money> manyMoney;

        public MoneyBox() { manyMoney = new List<Money>(); }

        // Business logic
        public Money Get(decimal denom)
        {
            int index = manyMoney.FindIndex(someMoney => someMoney.Amount == denom);
            Money result = manyMoney[index];
            manyMoney.RemoveAt(index);

            return result;
        }

        public void Put(List<Money> money)
        {
            foreach (var someMoney in money)
            {
                manyMoney.Add(someMoney);
            }
        }

        // Util methods
        public bool Empty()
        {
            return manyMoney.Count == 0;
        }

        public bool HasSome(decimal denom)
        {
            return HowMuch(denom) > 0;          
        }

        public int HowMuch(decimal denom)
        {
            return manyMoney.FindAll(someMoney => someMoney.Amount == denom).Count;
        }
    }
}
