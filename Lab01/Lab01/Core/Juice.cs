using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Core
{
    public class Juice
    {
        private List<String> extractedFrom;

        private Juice(List<String> ingredients)
        {
            this.extractedFrom = ingredients;
        }

        public Juice(String extractedFrom)
        {
            if (string.IsNullOrEmpty(extractedFrom))
            {
                throw new Exception("Juice initial ingredient name is empty.");
            }
            this.extractedFrom = new List<String>();
            this.extractedFrom.Add(extractedFrom);
        }

        public Juice Mix(Juice anotherJuice)
        {
            List<String> ingredients = GetIngredients().Concat(anotherJuice.GetIngredients()).ToList();
            return new Juice(ingredients);
        }

        public List<String> GetIngredients()
        {
            return extractedFrom;
        }

        public override String ToString()
        {
            return "Juice ingredients: " + String.Join(", ", GetIngredients().ToArray());
        }
    }
}
