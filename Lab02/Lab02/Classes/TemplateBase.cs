using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.Classes
{
    abstract class TemplateBase : FacadeClass
    {
        public void TemplateMethod()
        {
            // Making same sequence across all variants of this template
            SomeOperation1();
            SomeOperation2();
        }

        // This operations must be less specific than the one in Facade class
        protected abstract void SomeOperation1();

        protected abstract void SomeOperation2();
    }
}
