using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.Classes.Template
{
    class ConcreteClass : TemplateBase
    {
        protected override void SomeOperation1()
        {
            throw new NotImplementedException();
        }

        protected override void SomeOperation2()
        {
            throw new NotImplementedException();
        }
    }
}
