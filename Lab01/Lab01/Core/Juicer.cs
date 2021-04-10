using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab01.Core
{
    public class Juicer
    {
        private Juicer() { }

        private static Juicer instance;

        private static readonly object _lock = new object();

        private static bool inUse = false;

        private Glass glass;

        public static Juicer GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new Juicer();
                    }
                }
            }
            return instance;
        }

        public Glass Glass
        {
            get
            {
                lock (_lock)
                {
                    if (this.glass != null)
                    {
                        Glass glass = this.glass;
                        this.glass = null;
                        inUse = false;
                        return glass;
                    }
                    throw new Exception("There's no glass in juicer.");
                }
            }
            set
            {
                lock (_lock)
                {
                    if (this.glass == null)
                    {
                        this.glass = value;
                        inUse = true;
                    }
                    else
                    {
                        throw new Exception("There is already glass in the juicer.");
                    }
                }
            }
        }

        private Juice GetJuice(Juicy product)
        {
            return product.Juice;
        } 

        public async Task makeSomeJuice(Juicy product)
        {
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    if (this.glass != null)
                    {
                        if (product.IsPeeled() && product.IsSliced())
                        {
                            glass.Pour(GetJuice(product));
                        }
                        else
                        {
                            throw new Exception("Product isn't peeled and sliced.");
                        }
                    }
                    else
                    {
                        throw new Exception("There's no glass in juicer!");
                    }
                }
            });
        }

        public async Task makeGlassOfJuice(List<Juicy> products)
        {
            if (this.glass != null)
            {
                foreach (Juicy product in products)
                {
                    await makeSomeJuice(product);
                }
            } 
            else
            {
                throw new Exception("There's no glass in juicer!");
            }
        }

    }
}
