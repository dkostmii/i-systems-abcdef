using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    public class Toy
    {
        private decimal _price;
        private String _name;

        public String Name
        {
            get { return _name; }
            init
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("Name cannot be empty");
                }
                _name = value;
            }
        }

        public decimal Price
        {
            get { return _price; }
            init
            {
                if (value > 0)
                    _price = value;
                else
                    throw new Exception("Price cannot be negative");
            }
        }

        public override String ToString()
        {
            return $"Toy name: {Name}, toy price: {Price}";
        }
    }
}
