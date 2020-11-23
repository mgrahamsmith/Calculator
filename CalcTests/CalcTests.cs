using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestCalc1()
        {
            TestCalculate("9", "1", "+", "10");
        }

        [TestMethod]
        public void TestCalc2()
        {
            TestCalculate("4", "-7", "-", "11");
        }

        [TestMethod]
        public void TestCalc3()
        {
            TestCalculate("8", "8", "*", "64");
        }

        [TestMethod]
        public void TestCalc4()
        {
            TestCalculate("256", "0", "/", "ERROR: DIVIDE_BY_ZERO");
        }

        [TestMethod]
        public void TestCalc5()
        {
            TestCalculate("", "423", "+", "ERROR: INVALID_INPUT");
        }

        [TestMethod]
        public void TestCalc6()
        {
            TestCalculate("foo", "14", "+", "ERROR: INVALID_INPUT");
        }

        [TestMethod]
        public void TestOverflow()
        {
            TestCalculate("102423452345234523452345234", "11234524635342452345234523452345", "+", "ERROR: OVERFLOW");
        }

        [TestMethod]
        public void TestInputParse()
        {
            // Arrange
            CalculatorNS.Calculator calc = new CalculatorNS.Calculator();

            // Action
            string inputStr = "3+1+4/6*6*8/3-4*4+7";
            string actual = calc.parseInput(inputStr);

            // Assert
            string expected = "3 1 + 4 6 / 6 * 8 * 3 / + 4 4 * - 7 +";
            Assert.AreEqual(expected, actual, "FAIL: Failed to parse expression.");
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
