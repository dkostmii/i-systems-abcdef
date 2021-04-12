using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab03.Core;

namespace Lab03.G2D
{
    class G2DContext
    {
        private List<G2DLayer> layers;
        private G2DLayer current;

        public G2DContext()
        {
            layers = new List<G2DLayer>();
        }

        public void CreateLayer()
        {
            current = new G2DLayer();
            layers.Add(current);
        }

        public void DrawObjects(List<GObject> gObjects)
        {
            if (current != null)
            {
                foreach (var gObj in gObjects)
                {
                    current.PlaceObject(gObj);
                }
            }
            else
            {
                throw new Exception("Empty context error");
            }
        }

        public override String ToString()
        {
            String result = "[G2DContext] \n";
            if (layers.Count != 0)
            {
                foreach (var layer in layers)
                {
                    result += layer.ToString() + "\n";
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
