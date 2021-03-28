using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Util
{
    public class Randomize
    {
        private Randomize() { }

        /// <summary><para>Method <see cref="RandomCollection{T}(HashSet{T}, int, int)"/> creates random collection
        /// of size between <paramref name="minElements"/> and <paramref name="maxElements"/> from subset of given <paramref name="set"/> of elements.</para>
        /// <para>
        ///     <excepion cref="ArgumentOutOfRangeException">
        ///         Throws an <see cref="ArgumentOutOfRangeException"/> when <paramref name="minElements"/> > <paramref name="maxElements"/>
        ///     </excepion>.
        /// </para>
        /// </summary>
        /// <typeparam name="T">Type of elements of given hash set and resulting collection.</typeparam>
        /// <param name="set">Set of elements, which can be used.</param>
        /// <param name="minElements">Minimum size of subset of <c>set</c> and resulting collection.</param>
        /// <param name="maxElements"></param>
        /// 
        /// <returns>List of type <typeparamref name="T"/> with random elements.</returns>
        public static List<T> RandomCollection<T>(HashSet<T> set, int minElements, int maxElements)
        {
            if (minElements > maxElements)
            {
                throw new ArgumentOutOfRangeException("minElements");
            }


            Random random = new Random();
            int randomNumber = random.Next(minElements > 0 ? minElements : 1,
                    minElements == maxElements ? maxElements : maxElements - 1);

            HashSet<T> randomSubset = new HashSet<T>();
            for (int i = 0; i < randomNumber; i++)
            {
                randomSubset.Add(RandomItem<T>(set));
            }

            randomNumber = random.Next(minElements > 0 ? minElements : 1,
                minElements == maxElements ? maxElements : maxElements - 1);

            List<T> result = new List<T>();

            for (int i = 0; i < randomNumber; i++)
            {
                result.Add(RandomItem(randomSubset));
            }

            return result;
        }

        /// <summary>
        ///     Method <see cref="RandomItem{T}(HashSet{T})"/> returns random item from a given <paramref name="set"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in given <paramref name="set"/> and resulting random element.</typeparam>
        /// <param name="set">Set of elements to choose from.</param>
        /// <returns>Random element of type <typeparamref name="T"/>.</returns>
        public static T RandomItem<T>(HashSet<T> set)
        {
            Random random = new Random();
            int randomNumber = random.Next(set.Count);

            int currentIndex = 0;
            T randomItem = set.First();

            foreach (T element in set)
            {
                randomItem = element;

                if (currentIndex == randomNumber)
                    break;

                currentIndex++;
            }
            return randomItem;
        }
    }
}
