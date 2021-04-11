using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    class Toy
    {
        public String Name { get; init; }

        public decimal Price { get; init; }

        public override String ToString()
        {
            return $"Toy name: {Name}, toy price: {Price}";
        }
    }
}
