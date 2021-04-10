using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab01.Core;
using Lab01.Products;

namespace Lab01.Bags
{
    public class VegetableBag : ProductBag
    {
        public VegetableBag(HashSet<String> vegetables) : base(vegetables) { }

        public override Product TakeProduct(String product)
        {
            if (!products.Contains(product))
            {
                throw new Exception("What is that product?");
            }
            if (this.contents.Contains(product))
            {
                return new Vegetable(SearchForProduct(product));
            }
            throw new Exception($"Seems we don't have {product} in this bag :(");
        }
    }
}
