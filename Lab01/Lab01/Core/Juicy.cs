using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Core
{
    public abstract class Juicy : Product
    {
        private Juice juice;

        private bool isSliced;
        private bool isPeeled;

        public Juicy(String name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Name is empty");
            }
            isPeeled = false;
            isSliced = false;
            Name = name;
            juice = new Juice(name);
        }

        public Juice Juice
        {
            get
            {
                if (juice != null)
                {
                    Juice extracted = this.juice;
                    this.juice = null;
                    Console.WriteLine(extracted);
                    return extracted;
                }
                throw new Exception($"Juice is already extracted from {Name}");
            }
        }

        public Juicy Slice()
        {
            if (!this.isSliced)
                this.isSliced = true;

            return this;
        }

        public Juicy Peel()
        {
            if (!this.isPeeled)
                this.isPeeled = true;

            return this;
        }

        public bool IsSliced()
        {
            return this.isSliced;
        }

        public bool IsPeeled()
        {
            return this.isPeeled;
        }
    }
}
