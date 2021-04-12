using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03.Core
{
    interface GLayer
    {
        public GLayerIterator GetIterator();

        public void PlaceObject(GObject gObj);

        public void ClearLayer();
    }
}
