using System;

using System.Collections.Generic;

using Lab03.Core;
using Lab03.G2D;
using Lab03.G3D;

namespace Lab03
{
    class Program
    {
        static void Main(string[] args)
        {
            List<GObject> someObjects = new List<GObject>
            {
                new G2DObject { X = 2.3, Y = 5.6, Width = 10.2, Height = 8.6 },
                new G3DAdapter(
                    new G3DObject
                    {
                        X = 0.2,
                        Y = 3.1,
                        Z = -0.7,
                        Width = 4.9,
                        Height = 5.8,
                        Depth = 4.0
                    }
                 )
            };

            G2DContext context = new G2DContext();
            context.CreateLayer();
            context.DrawObjects(someObjects);

            Console.WriteLine(context.ToString());
        }
    }
}
