using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03.Core
{
    public interface GLayerIterator
    {
        public GObject GetNext();
        public bool HasMore();
        public void Reset();
    }
}
