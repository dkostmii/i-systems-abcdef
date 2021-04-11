using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace Lab02.ToyMachine
{
    delegate Dictionary<decimal, int> Summary(List<Money> money);
    class MoneyTransmitter
    {
        private Dictionary<decimal, int> moneyAmount;

        protected MoneyDispenser moneyDispenser;

        protected MoneyReceiver moneyReceiver;
        protected MoneyExtractor moneyExtractor;

        protected decimal credit;

        public MoneyTransmitter(MoneyReceiver moneyReceiver,
                         MoneyExtractor moneyExtractor,
                         HashSet<decimal> acceptsMoney,
                         MoneyDispenser dispenser)
        {
            moneyAmount = new Dictionary<decimal, int>();
            if (acceptsMoney.Count == 0)
            {
                throw new Exception("Valid money amounts set must not be empty.");
            }

            foreach (var amount in acceptsMoney)
            {
                moneyAmount.Add(amount, 0);
            }

            this.moneyDispenser = dispenser;

            this.moneyReceiver = moneyReceiver;
            this.moneyExtractor = moneyExtractor;

            credit = 0;
        }


        public decimal Credit
        {
            get { return credit; }
        }


        // Receiver mechanism
        public Money? ReceiveMoney(Money money)
        {
            if (!moneyAmount.ContainsKey(money.Amount))
                return money;

            moneyReceiver.PutSome(money);

            return null;
        }

        public void Replenish()
        {
            Dictionary<decimal, int> result = moneyReceiver.PushAll(CountMoney);

            foreach (KeyValuePair<decimal, int> moneyAmount in result)
            {
                this.moneyAmount[moneyAmount.Key] += moneyAmount.Value;
                this.credit += moneyAmount.Value;
            }
        }

        public List<Money> Cancel()
        {
            return moneyReceiver.ReturnAll();
        }



        // Extractor mechanism
        public void ExtractMoney()
        {
            Dictionary<decimal, int> creditEquiv = GetMoneyEquivalent();

            foreach(var amount in creditEquiv)
            {
                for (int i = 0; i < amount.Value; i++)
                {
                    moneyExtractor.ExtractSome(amount.Key);
                }
            }

            List<Money> money = moneyExtractor.PushAll();

            Dictionary<decimal, int> summary = CountMoney(money);
            Dictionary<decimal, int> saldo = new Dictionary<decimal, int>();

            foreach (var amount in summary)
            {
                saldo.Add(amount.Key, -amount.Value);
            }

            MoneyOperation(saldo);

            foreach (var someMoney in money)
            {
                moneyDispenser.Dispense(someMoney);
            }
        }

        

        // Operations
        public void CreditOperation(decimal saldo)
        {
            if (-saldo > credit)
                throw new Exception("Lack of requested credit");

            credit += saldo;
        }

        public void MoneyOperation(Dictionary<decimal, int> saldo)
        {
            // TODO: Update money state here
            foreach (KeyValuePair<decimal, int> amount in saldo)
            {
                if (!moneyAmount.ContainsKey(amount.Key))
                    throw new Exception($"Invalid amount of money requested: {amount.Value}");

                if (-amount.Value > moneyAmount[amount.Key])
                {
                    throw new Exception("Lack of requested money");
                }

                moneyAmount[amount.Key] += amount.Value;
            }
        }

        public Dictionary<decimal, int> CountMoney(List<Money> money)
        {
            if (money.Count > 0)
            {
                Dictionary<decimal, int> summary = new Dictionary<decimal, int>();
                foreach (var someMoney in money)
                {
                    if (summary.ContainsKey(someMoney.Amount))
                        summary[someMoney.Amount]++;
                    else
                        summary[someMoney.Amount] = 1;
                }

                return summary;
            }

            throw new Exception("Nothing to count");
        }

        public Dictionary<decimal, int> GetMoneyEquivalent()
        {
            Dictionary<decimal, int> result = new Dictionary<decimal, int>();

            decimal sum = credit;

            while (sum > 0 || sum > result.Keys.Min())
            {
                foreach (KeyValuePair<decimal, int> amount in moneyAmount)
                {
                    if (amount.Key < sum && amount.Value > 0)
                    {
                        sum -= amount.Value;
                        if (result.ContainsKey(amount.Key))
                        {
                            result[amount.Key]++;
                        }
                        else
                        {
                            result[amount.Key] = 1;
                        }
                    }
                }
            }

            return result;
        }
    }
}
