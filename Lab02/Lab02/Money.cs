using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    class Money : IEquatable<Money>
    {
        private decimal _amount;
        public decimal Amount 
        {
            get { return _amount; }
        }

        public Money(decimal amount)
        {
            if (amount > 0)
                _amount = amount;
            else
                throw new Exception("Money amount cannot be negative.");
        }


        // Generated
        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public bool Equals(Money other)
        {
            return other != null &&
                   Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount);
        }
    }
}
