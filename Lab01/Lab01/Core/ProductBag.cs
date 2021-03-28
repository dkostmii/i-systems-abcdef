using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Core
{
    public abstract class ProductBag
    {
        protected readonly HashSet<String> products;

        protected Stack<String> contents;

        protected ProductBag(HashSet<String> possibleProducts)
        {
            contents = new Stack<String>();
            products = possibleProducts;
        }

        public abstract Product TakeProduct(String name);

        protected String SearchForProduct(String product)
        {
            if (String.IsNullOrEmpty(product))
            {
                throw new Exception("Product name is empty.");
            }

            Stack<String> peeked = new Stack<string>();
            String result = null;
            while (!contents.Peek().Equals(product))
            {
                peeked.Push(contents.Pop());
            }
            if (contents.Any())
            {
                result = contents.Pop();
            }
            while (peeked.Any())
            {
                contents.Push(peeked.Pop());
            }
            if (String.IsNullOrEmpty(result))
                throw new Exception("Can't find {product} in ProductBag.");

            return result;
        }
        public void PutProduct(String name)
        {
            if (products.Contains(name))
            {
                contents.Push(name);
            }
            else
            {
                throw new Exception("What is that product?");
            }
        }

        public override String ToString()
        {
            if (contents.Any())
            {
                return "ProductBag with " + String.Join(", ", contents.ToArray());
            }
            return "Empty ProductBag";
        }
    }
}
