using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab03.Core;

namespace Lab03.G3D
{
    class G3DObject : GObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }

        public void Scale(double factor)
        {
            ScaleX(factor);
            ScaleY(factor);
            ScaleZ(factor);
        }

        public void ScaleX(double factor)
        {
            Width = factor * Width;
        }

        public void ScaleY(double factor)
        {
            Height = factor * Height;
        }

        public void ScaleZ(double factor)
        {
            Depth = factor * Depth;
        }

        public override String ToString()
        {
            return $"3D object pos: [{X}, {Y}, {Z}]; Size: {Width}, {Height}, {Depth}";
        }
    }
}
