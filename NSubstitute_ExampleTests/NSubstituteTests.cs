using System;
using System.Text;
using System.Collections.Generic;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute_Example;

namespace NSubstitute_ExampleTests
{
    /// <summary>
    /// NSubstituteTests 的摘要描述
    /// </summary>
    [TestClass]
    public class NSubstituteTests
    {
        public NSubstituteTests()
        {
            //
            // TODO:  在此加入建構函式的程式碼
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///取得或設定提供目前測試回合
        ///的相關資訊與功能的測試內容。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 其他測試屬性
        //
        // 您可以使用下列其他屬性撰寫您的測試: 
        //
        // 執行該類別中第一項測試前，使用 ClassInitialize 執行程式碼
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在類別中的所有測試執行後，使用 ClassCleanup 執行程式碼
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在執行每一項測試之前，先使用 TestInitialize 執行程式碼 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在執行每一項測試之後，使用 TestCleanup 執行程式碼
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_CheckReceivedCalls_CallReceived()
        {
            var command = Substitute.For<ICommand>();
            var something = new SomethingThatNeedsACommand(command);

            something.DoSomething();
            command.Received().Execute();
        }

        [TestMethod]
        public void Test_CheckReceivedCalls_CallDidNotReceived()
        {
            var command = Substitute.For<ICommand>();
            var something = new SomethingThatNeedsACommand(command);

            something.DontDoAnything();

            command.DidNotReceive().Execute();
        }

        [TestMethod]
        public void Test_CheckReceivedCalls_CallReceivedNumberOfSpecifiedTimes()
        {
            var command = Substitute.For<ICommand>();
            var repeter = new CommandRepeater(command, 3);

            repeter.Execute();

            command.Received(3).Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(NSubstitute.Exceptions.ReceivedCallsException))]
        public void Test_CheckReceivedCalls_CallReceivedNumberOfSpecifiedTimes_Exception()
        {
            var command = Substitute.For<ICommand>();
            var repeter = new CommandRepeater(command, 3);

            repeter.Execute();

            command.Received(4).Execute();
        }

        [TestMethod]
        public void Test_CheckReceivedCalls_CallReceivedWithSpecificArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(1, 2);
            calculator.Add(-100, -200);

            calculator.Received().Add(Arg.Any<int>(), 2);
            calculator.Received().Add(Arg.Is<int>(x => x > 0), 2);
        }

        [TestMethod]
        public void Test_CheckReceivedCalls_IgnoringArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(1, 1);
            calculator.ReceivedWithAnyArgs().Add(2, 3);
        }

        [TestMethod]
        public void Test_CheckReceivedCalls_CheckingEventSubscriptions()
        {
            var command = Substitute.For<ICommand>();
            var watcher = new CommandWatcher(command);

            command.Executed += Raise.Event();

            Assert.IsTrue(watcher.DidStuff);
        }
    }
}
