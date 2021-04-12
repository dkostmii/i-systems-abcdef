using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab03.Core;
using Lab03.G3D;

namespace Lab03.G2D
{
    class G3DAdapter : G2DObject
    {
        G3DObject adaptee;

        public new double X 
        {
            get { return adaptee.X; }
            set { adaptee.X = value; }
        }

        public new double Y
        {
            get { return adaptee.Y; }
            set { adaptee.Y = value; }
        }

        public new double Width
        {
            get { return adaptee.Width; }
            set { adaptee.Width = value; }
        }

        public new double Height
        {
            get { return adaptee.Height; }
            set { adaptee.Height = value; }
        }

        public G3DAdapter(G3DObject adaptee)
        {
            this.adaptee = adaptee;
        }

        public override void Scale(double factor) 
        {
            adaptee.Scale(factor);
        }

        public override String ToString()
        {
            return adaptee.ToString();
        }
    }
}