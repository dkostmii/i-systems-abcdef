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
    public class ToyTrayTest
    {
        [TestMethod]
        public void EmptyMethodWorksProperly()
        {
            ToyTray toyTray = new ToyTray();
            Assert.IsTrue(toyTray.Empty());
        }

        [TestMethod]
        public void GettingFromEmptyTrayThrows()
        {
            ToyTray toyTray = new ToyTray();
            Assert.IsTrue(toyTray.Empty());

            Assert.ThrowsException<Exception>(() =>
            {
                toyTray.GetToys();
            });
        }
    }
}
