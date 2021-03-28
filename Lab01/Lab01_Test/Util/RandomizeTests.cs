using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Lab01.Util.Randomize;


namespace Lab01_Test.Util
{
    [TestClass]
    public class RandomizeTests
    {

        [TestMethod]
        public void MinValueGtMaxValueThrows()
        {
            HashSet<String> set = new HashSet<String>();
            const int minValue = 2;
            const int maxValue = 1;

            set.Add("Mango");
            set.Add("Apple");
            set.Add("Lemon");
            set.Add("Orange");

            if (!(minValue > maxValue))
            {
                throw new Exception($"minValue is not greater than maxValue: {minValue} <= {maxValue}.");
            }

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                RandomCollection<String>(set, minValue, maxValue);
            });
        }

        [TestMethod]
        public void ReturnsSameElement()
        {
            HashSet<String> set = new HashSet<String>();

            String item = "Banana";

            set.Add(item);

            if (set.Count != 1)
            {
                throw new Exception($"Set doesn't have exactly 1 element: {set.Count}.");
            }

            Assert.IsTrue(set.Contains(item) && set.Count == 1);

            set.Remove(item);

            Assert.IsFalse(set.Contains(item) && set.Count == 0);
        }
    }
}
