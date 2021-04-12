using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab02;
using Lab02.ToyMachine;

namespace Lab02_Test.ToyMachine
{
    [TestClass]
    public class ToyBoxTest
    {
        [TestMethod]
        public void EmptyMethodWorksProperly()
        {
            ToyBox toyBox = new ToyBox();
            Assert.IsTrue(toyBox.Empty());
        }

        [TestMethod]
        public void GettingFromEmptyBoxThrows()
        {
            ToyBox toyBox = new ToyBox();
            Assert.IsTrue(toyBox.Empty(), "ToyBox is not empty");

            Assert.ThrowsException<Exception>(() =>
            {
                toyBox.GetToy();
            });
        }
    }
}
