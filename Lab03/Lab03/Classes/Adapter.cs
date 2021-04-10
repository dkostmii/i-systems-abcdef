using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab03.Classes;

namespace Lab03.Classes
{
    class Adapter : Target
    {
        private Adaptee adaptee;
        public new void Operation()
        {
            adaptee.SomeSpecificOperation();
        }
    }
}
