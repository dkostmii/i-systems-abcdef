using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab03.Core;

namespace Lab03.G2D
{
    // Custom aggregate
    public class G2DLayer : GLayer
    {
        private Stack<GObject> contents;

        public G2DLayer()
        {
            contents = new Stack<GObject>();
        }

        public GLayerIterator GetIterator()
        {
            return new G2DLayerIterator(contents.GetEnumerator());
        }

        public void PlaceObject(GObject g2DObj)
        {
            contents.Push(g2DObj);
        }

        public void ClearLayer()
        {
            contents.Clear();
        }

        public override String ToString()
        {
            String result = "[G2DLayer]\n";
            if (contents.Count != 0)
            {
                foreach (var gObj in contents)
                {
                    result += gObj.ToString() + "\n";
                }
            }
            else
            {
                result += "Empty";
            }

            return result;
        }
    }
}
