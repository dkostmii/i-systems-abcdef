using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.ToyMachine
{
    public class ToyBox
    {
        private List<Toy> toys;

        public ToyBox() { toys = new List<Toy>(); }

        public void PutToys(List<Toy> toys)
        {
            foreach (var toy in toys)
            {
                this.toys.Add(toy);
            }
        }

        // Default implementation
        // which gives the random toy
        public Toy GetToy()
        {
            if (toys.Any())
            {
                int rand = new Random().Next(0, toys.Count);

                Toy randomToy = toys.ElementAt(rand);

                toys.RemoveAt(rand);

                return randomToy;
            }

            throw new Exception("Toy box is empty!");
        }

        // Util methods

        public bool Empty()
        {
            return toys.Count == 0;
        }
    }
}
