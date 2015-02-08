using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute_Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
namespace NSubstitute_Example.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void AddTest()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(3, 5).Returns(8);
            var actual =  calculator.Add(3, 5);
            var expectd = 8;
            Assert.AreEqual(expectd, actual);
        }

        [TestMethod()]
        public void TestNsubstituteReceive()
        {
            var calculator = Substitute.For<ICalculator>();
            var tmp = calculator.Add(2, 5);

            calculator.Received().Add(2, 5);
        }

        [TestMethod()]
        [ExpectedException(typeof(NSubstitute.Exceptions.ReceivedCallsException))]
        public void TestNsubstituteReceive_Exception()
        {
            var calculator = Substitute.For<ICalculator>();
            var tmp = calculator.Add(2, 5);

            calculator.Received().Add(3, 5);
        }
    }
}
