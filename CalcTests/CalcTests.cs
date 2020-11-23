using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Test1()
        {
            TestCalculate("9", "1", "+", "10");
        }

        [TestMethod]
        public void Test2()
        {
            TestCalculate("4", "-7", "-", "11");
        }

        [TestMethod]
        public void Test3()
        {
            TestCalculate("8", "8", "*", "64");
        }

        [TestMethod]
        public void Test4()
        {
            TestCalculate("256", "0", "/", "ERROR: DIVIDE_BY_ZERO");
        }

        [TestMethod]
        public void Test5()
        {
            TestCalculate("", "423", "+", "ERROR: INVALID_INPUT");
        }

        [TestMethod]
        public void Test6()
        {
            TestCalculate("foo", "14", "+", "ERROR: INVALID_INPUT");
        }

        [TestMethod]
        public void TestOverflow()
        {
            TestCalculate("102423452345234523452345234", "11234524635342452345234523452345", "+", "ERROR: OVERFLOW");
        }

        public void TestCalculate(string leftVal, string rightVal, string operatorStr, string expected)
        {
            // Arrange
            CalculatorNS.Calculator calc = new CalculatorNS.Calculator();

            // Action
            calc.setLeft(leftVal);
            calc.setRight(rightVal);
            calc.setOperator(operatorStr);
            calc.calculate();

            // Assert
            string actual = calc.getResult();
            Assert.AreEqual(expected, actual, "FAIL: Calculation incorrect.");
        }
    }
}
