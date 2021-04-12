using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.ToyMachine
{
    public class ToyTray
    {
        List<Toy> toys;
        public ToyTray()
        {
            toys = new List<Toy>();
        }

        public void PutToy(Toy toy)
        {
            toys.Add(toy);
        }

        public List<Toy> GetToys()
        {
            if (toys.Count > 0)
            {
                List<Toy> result = toys;
                toys = new List<Toy>();
                return result;
            }
                

            throw new Exception("Tray is empty");
        }

        public bool Empty()
        {
            return toys.Count == 0;
        }
    }
}
