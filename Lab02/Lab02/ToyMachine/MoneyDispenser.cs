using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.ToyMachine
{
    delegate List<Money> Dispense();
        
    class MoneyDispenser
    {
        private List<Money> money;

        public MoneyDispenser() 
        {
            money = new List<Money>();
        }

        public void Dispense(Money money)
        {
            this.money.Add(money);
        }

        public List<Money> GetMoney()
        {
            if (money.Count > 0)
                return money;

            throw new Exception("Money dispenser is empty");
        }

    }
}
