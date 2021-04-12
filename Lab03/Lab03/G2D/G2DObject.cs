using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab03.Core;

namespace Lab03.G2D
{
    public class G2DObject : GObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public virtual void Scale(double factor)
        {
            ScaleX(factor);
            ScaleY(factor);
        }

        public void ScaleX(double factor)
        {
            Width = factor * Width;
        }

        public void ScaleY(double factor)
        {
            Height = factor * Height;
        }

        public override String ToString()
        {
            return $"2D object pos: {X}, {Y}; Size: {Width}, {Height}";
        }
    }
}
