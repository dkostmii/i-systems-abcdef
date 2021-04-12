using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab03.Core;

namespace Lab03.G2D
{
    class G2DLayerIterator : GLayerIterator
    {
        private IEnumerator<GObject> enumerator;
        private bool reachedEnd;

        public G2DLayerIterator(IEnumerator<GObject> enumerator)
        {
            this.enumerator = enumerator;
            this.reachedEnd = false;
        }

        public GObject GetNext()
        {
            reachedEnd = !enumerator.MoveNext();
            return enumerator.Current;
        }

        public bool HasMore()
        {
            return !reachedEnd;
        }

        public void Reset()
        {
            enumerator.Reset();
        }
    }
}
